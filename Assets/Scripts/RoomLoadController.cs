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
		InitRoomObject(roomName+currStackPos.ToString(),currStackPos);
		AddRoom("RoomRight");
		//AddRoom("RoomLeft");
	}

	public void AddRoom (string roomName){
		currStackPos=1;
		InitRoomObject(roomName+currStackPos.ToString(),currStackPos);
		roomLoader.Load(roomName);
	}

	public void RemoveRoom (string roomName){
		roomLoader.Unload(roomName);
	}

	void InitRoomObject (string roomName, int stackPos){
		roomPrefab = Instantiate(roomPrefab) as Room;
		roomPrefab.name= roomName+currStackPos.ToString();
		roomPrefab.SetStackPos(currStackPos);
	}
}
