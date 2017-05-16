using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
	[Header("Ko Elwin")]
	public string name;
	public string message;
	public ItemSO item;
	public bool triggerInteract;

	[Header("Reference")]
	public NPCTextMessage bubbleText;

	int counter = 0	;

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			if(triggerInteract){
				triggerInteract = false;
				Interact();
			}
		}
	}

	public void Interact(){
		Talk();
		GiveItem();
	}

	public void Talk(){
		bubbleText.Show(message,name);
		GiveItem();
	}

	void GiveItem(){
		if(item != null){
			//give item to player
		}
	}

}
