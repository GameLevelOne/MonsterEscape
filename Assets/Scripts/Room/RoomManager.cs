using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
	public RoomDataSO[] rooms;

	public RoomLoader roomLoader;
	public GameObject playerObj;
	public Fader fader;

	private RoomDataSO currRoom;
	private float xPos = 0f;
	private int currStackPos = 0;

	// Use this for initialization
	void Start () {
		Init();

	}

	void Init(){
		currRoom = rooms[(int)RoomNames.Room6Temp];
		roomLoader.Load(RoomNames.Room6Temp);
		LoadAdjacentRooms(currRoom);
		SetStackPos();
	}

	void LoadAdjacentRooms (RoomDataSO targetRoom){
		for (int i = 0; i < targetRoom.adjacentRoomNames.Length; i++) {
			if ( targetRoom.adjacentRoomNames [i] != currRoom.roomName && targetRoom.adjacentRoomNames [i] != RoomNames.NULL) {
				roomLoader.Load(targetRoom.adjacentRoomNames[i]);
			}
		}
	}

	public void RemoveRoom (RoomNames roomName){
		roomLoader.Unload(roomName);
	}

	public void CheckUnload(){
		for (int i = 0; i < rooms.Length; i++) {
			if (rooms[i].stackPos == 2) {
				roomLoader.Unload (rooms[i].roomName);
			}
		}
	}

	public void SetStackPos ()
	{
//		for (int i = 0; i < rooms.Length; i++) {
//			if (i != currSceneIdx) {
//				if (rooms [i].stackPos < 2)
//					rooms [i].stackPos++;
//				else
//					rooms [i].stackPos = 2;
//			} else {
//				rooms [i].stackPos = 0;
//			}
//		}

		currRoom.stackPos = 0;

		for (int i = 0; i < rooms.Length; i++) {
			if (rooms [i].stackPos == 1) {
				rooms [i].stackPos = 2;
			} else if (rooms [i].stackPos >= 2) {
				rooms [i].stackPos = 0;
			}
		}

		for (int i = 0; i < currRoom.adjacentRoomNames.Length; i++) {
			if (currRoom.adjacentRoomNames [i] != RoomNames.NULL)
				rooms [(int)currRoom.adjacentRoomNames [i]].stackPos = 1;
		}

		if (currRoom.roomName == RoomNames.Room12) {
			rooms[(int)RoomNames.Room7Temp].stackPos=2;
		}
		if (currRoom.roomName == RoomNames.Room2) {
			rooms[(int)RoomNames.Room11].stackPos=2;
		}
	}

	public void ChangeScene (RoomDataSO targetRoom)
	{
//		if (targetRoom == RoomNames.RoomLeft) {
//			SetStackPos (0);
//		} else if (targetRoom == RoomNames.RoomMiddle) {
//			SetStackPos (1);
//
//			if (rooms [(int)RoomNames.RoomLeft].stackPos == 2) {
//				roomLoader.Load (RoomNames.RoomLeft.ToString ());
//			} else if (rooms [(int)RoomNames.RoomRight].stackPos == 2) {
//				roomLoader.Load (RoomNames.RoomRight.ToString ());
//			}
//
//		} else if (targetRoom == RoomNames.RoomRight) {
//			SetStackPos(2);
//		}
		LoadAdjacentRooms(targetRoom);
		currRoom = targetRoom;
		SetStackPos();
		CheckUnload();

		Camera.main.transform.localPosition = currRoom.cameraPos;
		playerObj.transform.localPosition = currRoom.playerSpawnPos;
		fader.FadeIn();
	}
}
