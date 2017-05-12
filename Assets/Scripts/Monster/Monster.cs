using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterAnimState {	
	IDLE, 
	WALK, 
	RUN, 
	ATTACK, 
	CONFUSED, 
	DAMAGED 
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

	const float PATROL_SPEED = 5f;
	const float SEARCH_SPEED = 7.5f;
	const float RUN_SPEED = 10f;

	public MonsterState currentState;
	public Transform[] monsterWaypoint;
	public int currentWaypoint = 0;
	Vector3 searchLocation;
	Vector3 startPosition;

	void Start() {
		monsterAnim = GetComponent<Animator> ();
		monsterSprite = GetComponent<SpriteRenderer> ();
		startPosition = transform.position;

		CheckPatrol ();
	}
	void CheckPatrol() {
		if (monsterWaypoint.Length > 0)
			currentState = MonsterState.PATROL;
		else
			currentState = MonsterState.IDLE;
	}

	void FixedUpdate() {
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
		}
	}

	public void TestHear(GameObject g) {
		HearSomething (g.transform.position);
	}

	public void HearSomething(Vector3 hearLocation) {
		searchLocation = hearLocation;
		monsterSprite.flipX = GetDirection (searchLocation);
		currentState = MonsterState.AWARE;
		AnimChange (MonsterAnimState.DAMAGED);
		StartCoroutine (DelayToNextState(1f,MonsterState.CAREFUL));
	}
	void GetConfused() {
		currentState = MonsterState.NOTFOUND;
		AnimChange (MonsterAnimState.CONFUSED);
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
		monsterSprite.flipX = GetDirection(target);

		transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.x,transform.position.y,transform.position.y),Time.deltaTime * speed);
	}

	bool GetDirection(Vector3 target){
		if(transform.position.x > target.x) 
			return false;
		else 
			return true;
	}	

	bool IsArrived(Vector3 target) {
		if (((monsterSprite.flipX) && (transform.position.x >= target.x)) || ((!monsterSprite.flipX) && (transform.position.x <= target.x)))
			return true;
		else
			return false;
	}
}
