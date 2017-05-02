using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
	public RoomLoadController roomLoadController;
	public string roomName;

	private bool isLoaded = false;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			if (!isLoaded) {
				roomLoadController.AddRoom (roomName);
				isLoaded=true;
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Player") {
			isLoaded=false;
		}
	}
}
