using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAble : Properties {
	
	public GameObject player;

	void Start(){
		Init();
	}

	void Init(){}

	void OntriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			//register player action
			player = p.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			//unregister player action
			player = null;
		}
	}

	void OnPlayerAction(){
		if(player.activeSelf) player.SetActive(false);
		else if(!player.activeSelf) player.SetActive(true);
	}
}
