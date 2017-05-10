using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterAnimState {	
	IDLE, 
	RUN, 
	WALK, 
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
	ATTACK
}

public class Monster : MonoBehaviour {
	enum Direction{LEFT, RIGHT}
	Direction direction;

	MonsterAnimState state;
	Animator anim;

	Transform player;
	public Transform Player{
		get{ return this.player; }
		set{ this.player = value; }
	}


	public GameObject[] Waypoints;
	public float speed;
	float walkSpeed, RunSpeed;
	float tIdle;
	int current;
	bool idling;

	void Start(){
		anim = GetComponent<Animator>();
		state = MonsterAnimState.IDLE;
		current = 0;
		tIdle = 0f;
		walkSpeed = speed;
		RunSpeed = speed * 3;
		state = MonsterAnimState.WALK;
		idling = false;
	}

	public MonsterAnimState State{
		get{ return this.state; }
	}

	public void SetAnimState(MonsterAnimState state){
		this.state = state;
		anim.SetInteger("AnimState",(int)this.state);
	}

	public void SetIdle(){
		StartCoroutine(ToIdle());
	}

	IEnumerator ToIdle(){
		yield return new WaitForSeconds(2f);
		state = MonsterAnimState.IDLE;
		anim.SetInteger("AnimState",(int)state);
	}

	void Update(){
		if(state == MonsterAnimState.IDLE){
			if(idling == false){
				idling = true;
				StartCoroutine(Idle());
			}
		}
		if(state == MonsterAnimState.WALK){
			if(Mathf.Abs(transform.position.x - Waypoints[current].transform.position.x) >= 0.1f){
				speed = walkSpeed;
				MoveToDestination(Waypoints[current].transform);
			}else{
				if(state != MonsterAnimState.IDLE) SetAnimState(MonsterAnimState.IDLE);
			}
		}
	}

	IEnumerator Idle(){
		yield return new WaitForSeconds(2.5f);
		current++;
		if(current == Waypoints.Length) current = 0;
		SetAnimState(MonsterAnimState.WALK);
		idling = false;

	}

	void MoveToDestination(Transform target){
		direction = GetDirection(target);
		if(direction == Direction.LEFT) GetComponent<SpriteRenderer>().flipX = false;
		else GetComponent<SpriteRenderer>().flipX = true;

		transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.position.x,transform.position.y,target.position.z),Time.deltaTime * speed);
	}

	Direction GetDirection(Transform target){
		if(transform.position.x > target.position.x) return Direction.LEFT;
		else return Direction.RIGHT;
	}
}
