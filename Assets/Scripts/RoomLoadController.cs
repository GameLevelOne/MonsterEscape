using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoadController : MonoBehaviour {
	public RoomLoader roomLoader;
	public Room roomPrefab;

	private string roomName = "room";
	private float xPos = 0f;
	private int currStackPos = 0;

	void Awake(){
		roomLoader = Instantiate(roomLoader) as RoomLoader;
	}

	// Use this for initialization
	void Start () {
		AddRooms();
		//AddRoom("RoomLeft");

	}

	public void AddRooms ()
	{
		Scene scene = SceneManager.GetActiveScene ();

		if (scene.name == RoomNames.RoomMiddle.ToString ()) {
			roomLoader.Load (RoomNames.RoomLeft.ToString ());
			roomLoader.Load (RoomNames.RoomRight.ToString ());
		} else if (scene.name == RoomNames.RoomLeft.ToString ()) {
			roomLoader.Load (RoomNames.RoomMiddle.ToString ());
		} else if(scene.name == RoomNames.RoomRight.ToString()){
			roomLoader.Load(RoomNames.RoomMiddle.ToString());
		}
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
