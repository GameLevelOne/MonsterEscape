using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoadController : MonoBehaviour {
	public RoomLoader roomLoader;
	public Room roomPrefab;

	private string roomName = "room";
	private float xPos = 0f;
	private int currStackPos = 0;

	// Use this for initialization
	void Start () {
		roomPrefab = Instantiate(roomPrefab) as Room;
		roomPrefab.name= roomName+currStackPos.ToString();
		roomPrefab.SetStackPos(currStackPos);
	}
	
	public void AddRoom (string roomName){

			
		roomLoader.Load(roomName);
	}

	void InitRoomObject (string roomName, int stackPos){
		
	}
}
