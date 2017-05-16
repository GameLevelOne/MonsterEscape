using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterAnimState {	
	IDLE, 
	WALK, 
	RUN, 
	ATTACK, 
	CONFUSED, 
	DAMAGED,
	SURPRISED
}
public enum MonsterState {	
	IDLE, 
	PATROL, 
	RETURN, 
	AWARE, 
	CAREFUL,
	CHASE,
	SEARCH,
	NOTFOUND,
	ATTACK
}

public class Monster : MonoBehaviour {

	Animator monsterAnim;
	SpriteRenderer monsterSprite;

	//original values
//	const float PATROL_SPEED = 5f;
//	const float SEARCH_SPEED = 7.5f;
//	const float RUN_SPEED = 15f;

	const float PATROL_SPEED = 1f;
	const float SEARCH_SPEED = 1.5f;
	const float RUN_SPEED = 3f;

	public MonsterState currentState;
	public Transform[] monsterWaypoint;
	public int currentWaypoint = 0;
	public TriggerCollider leftSight;
	public TriggerCollider rightSight;
	public delegate void GameOver();
	public static event GameOver DoGameOver;
	Player playerTarget;
	Vector3 searchLocation;
	Vector3 startPosition;
	Vector3 playerLocation;
	bool playerEntered=false;

	void Start() {
		monsterAnim = GetComponent<Animator> ();
		monsterSprite = GetComponent<SpriteRenderer> ();
		startPosition = transform.position;

		leftSight.OnTriggerEnter += OnPlayerSighted;
		rightSight.OnTriggerEnter += OnPlayerSighted;
		leftSight.OnTriggerExit += OnPlayerHidden;
		rightSight.OnTriggerExit += OnPlayerHidden;

		SetDirection (transform.position);
		CheckPatrol ();
	}
	void CheckPatrol() {
		if (monsterWaypoint.Length > 0)
			currentState = MonsterState.PATROL;
		else
			currentState = MonsterState.IDLE;
	}

	void Update() {
		if (currentState == MonsterState.IDLE) {
			AnimChange (MonsterAnimState.IDLE);
		} else if (currentState == MonsterState.PATROL) {
			AnimChange (MonsterAnimState.WALK);
			MoveToDestination (monsterWaypoint [currentWaypoint].position);
			if (IsArrived (monsterWaypoint [currentWaypoint].position)) {
				currentWaypoint++;
				if (currentWaypoint >= monsterWaypoint.Length) {
					currentWaypoint = 0;
				}
			}
		} else if (currentState == MonsterState.CAREFUL) {
			AnimChange (MonsterAnimState.WALK);
			MoveToDestination (searchLocation,SEARCH_SPEED);
			if (IsArrived (searchLocation)) {
				GetConfused ();
			}
		} else if (currentState == MonsterState.RETURN) {
			AnimChange (MonsterAnimState.WALK);
			MoveToDestination (startPosition,SEARCH_SPEED);
			if (IsArrived (startPosition)) {
				CheckPatrol ();
			}
		} else if (currentState == MonsterState.CHASE) {
			AnimChange (MonsterAnimState.RUN);
			if (playerTarget != null) {
				MoveToDestination (playerTarget.transform.position,RUN_SPEED);
				if (IsArrived (playerTarget.transform.position)) {
					GetConfused ();
				}
			} else {
				MoveToDestination (playerLocation,RUN_SPEED);
				if (IsArrived (playerLocation)) {
					GetConfused ();
				}
			}
		}
	}

	public void TestHear(GameObject g) {
		HearSomething (g.transform.position);
	}

	public void HearSomething(Vector3 hearLocation) {
		searchLocation = hearLocation;
		SetDirection (searchLocation);
		currentState = MonsterState.AWARE;
		AnimChange (MonsterAnimState.SURPRISED);
		StartCoroutine (DelayToNextState(1.5f,MonsterState.CAREFUL));
	}
	void GetConfused() {
		currentState = MonsterState.NOTFOUND;
		AnimChange (MonsterAnimState.CONFUSED,0.5f);
		StartCoroutine (DelayToNextState(3f,MonsterState.RETURN));
	}
	IEnumerator DelayToNextState(float delay, MonsterState nextState) {
		yield return new WaitForSeconds (delay);
		currentState = nextState;
	}
		
	public void SetIdle(){
		AnimChange (MonsterAnimState.IDLE);
	}

	void AnimChange(MonsterAnimState ms, float animSpeed = 1f)
	{
		monsterAnim.enabled = true;
		monsterAnim.SetInteger ("AnimState",(int)ms);
		monsterAnim.speed = animSpeed;
	}

	void MoveToDestination(Vector3 target, float speed = PATROL_SPEED){
		SetDirection(target);

		transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.x,transform.position.y,transform.position.y),Time.deltaTime * speed);
	}

	void SetDirection(Vector3 target){
		if (transform.position.x >= target.x) {
			monsterSprite.flipX = false;
			leftSight.gameObject.SetActive (true);
			rightSight.gameObject.SetActive (false);
		} else {
			monsterSprite.flipX = true;
			leftSight.gameObject.SetActive (false);
			rightSight.gameObject.SetActive (true);
		}
	}	

	bool IsArrived(Vector3 target) {
		if (((monsterSprite.flipX) && (transform.position.x >= target.x)) || ((!monsterSprite.flipX) && (transform.position.x <= target.x)))
			return true;
		else
			return false;
	}

	void OnPlayerSighted(GameObject other) {
		playerTarget = other.GetComponent<Player>();
		SetDirection (playerLocation);
		if (currentState != MonsterState.CHASE) {
			currentState = MonsterState.AWARE;
			AnimChange (MonsterAnimState.SURPRISED);
			StartCoroutine (DelayToNextState (1.5f, MonsterState.CHASE));
		}
	}
	void OnPlayerHidden(GameObject other) {
		playerLocation = other.transform.position;
		playerTarget = null;
		SetDirection (playerLocation);
		if (currentState != MonsterState.CHASE) {
			currentState = MonsterState.AWARE;
			AnimChange (MonsterAnimState.SURPRISED);
			StartCoroutine (DelayToNextState (1.5f, MonsterState.CHASE));
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" && !playerEntered) {
			Debug.Log ("Game Over");
			playerEntered=true;
			currentState = MonsterState.IDLE;
			DoGameOver();
		}
	}

	void OnDestroy() {
		leftSight.OnTriggerEnter -= OnPlayerSighted;
		rightSight.OnTriggerEnter -= OnPlayerSighted;
		leftSight.OnTriggerExit -= OnPlayerHidden;
		rightSight.OnTriggerExit -= OnPlayerHidden;
	}
}
