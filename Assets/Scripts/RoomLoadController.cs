using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoadController : MonoBehaviour {
	public RoomLoader roomLoader;

	private float xPos = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	public void AddRoom (string roomName){
		//xPos += 8f;

		roomLoader.Load(roomName);
	}
}
