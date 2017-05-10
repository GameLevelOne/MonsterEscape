using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
	[Header("Ko Elwin")]
	public string name;
	public string[] message;
	public ItemSO item;
	public bool triggerInteract;

	[Header("Reference")]
	public NPCTextMessage bubbleText;
	public RectTransform canvas;

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
		GameObject tempBubbleText = Instantiate(bubbleText.gameObject) as GameObject;

		tempBubbleText.GetComponent<RectTransform>().SetParent(canvas);
		tempBubbleText.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		tempBubbleText.GetComponent<RectTransform>().localScale = Vector3.one;

		tempBubbleText.GetComponent<NPCTextMessage>().Show(message[counter], name);

		if(counter != message.Length-1) counter++;
	}

	void GiveItem(){
		if(item != null){
			//give item to player
		}
	}

}
