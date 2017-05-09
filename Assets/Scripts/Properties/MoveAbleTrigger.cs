using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbleTrigger : MonoBehaviour {	
	public PlayerMoveSide pos;
	MoveAble parent;
	void Start(){
		parent = transform.parent.GetComponent<MoveAble>();
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			//register OnPlayerAction
			parent.playerPullPos = pos;
			parent.player = p.transform;
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			//unregister OnPlayerAction
			parent.player = null;
		}
	}

	void OnPlayerAction(){
		parent.OnPlayerAction();
	}
}
