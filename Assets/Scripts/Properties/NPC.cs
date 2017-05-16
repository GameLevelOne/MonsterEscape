using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
	[Header("Ko Elwin")]
	public string name;
	public string message;
	public ItemSO item;

	[Header("Reference")]
	public NPCTextMessage bubbleText;

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
