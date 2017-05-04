using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState{	IDLE, RUN, WALK, ATTACK, CONFUSED, DAMAGED }

public class Monster : MonoBehaviour {
	enum Direction{LEFT, RIGHT}
	Direction direction;

	AnimState state;
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
		InitMonster();
	}

	void InitMonster(){
		anim = GetComponent<Animator>();
		state = AnimState.IDLE;
		current = 0;
		tIdle = 0f;
		walkSpeed = speed;
		RunSpeed = speed * 3;
		state = AnimState.WALK;
		idling = false;

	}

	public AnimState State{
		get{ return this.state; }
	}

	public void SetAnimState(AnimState state){
		this.state = state;
		anim.SetInteger("AnimState",(int)this.state);
	}

	public void SetIdle(){
		StartCoroutine(ToIdle());
	}

	IEnumerator ToIdle(){
		yield return new WaitForSeconds(2f);
		state = AnimState.IDLE;
		anim.SetInteger("AnimState",(int)state);
	}

	void Update(){
		if(state == AnimState.IDLE){
			if(idling == false){
				idling = true;
				StartCoroutine(Idle());
			}
		}
		if(state == AnimState.WALK){
			if(Mathf.Abs(transform.position.x - Waypoints[current].transform.position.x) >= 0.1f){
				speed = walkSpeed;
				MoveToDestination(Waypoints[current].transform);
			}else{
				if(state != AnimState.IDLE) SetAnimState(AnimState.IDLE);
			}
		}
	}

	IEnumerator Idle(){
		yield return new WaitForSeconds(2.5f);
		current++;
		if(current == Waypoints.Length) current = 0;
		SetAnimState(AnimState.WALK);
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
