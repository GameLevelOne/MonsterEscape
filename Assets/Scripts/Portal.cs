using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public RoomLoadController roomLoadController;
	public string roomName;

	void OnTrigger2DEnter (Collider2D col)
	{
		if (col.tag == "Player") {
			if (roomName == "roomLeft") {
				Debug.Log ("roomLeft");
			} else if(roomName == "roomRight"){
				Debug.Log("roomRight");
			}
		}
	}
}
