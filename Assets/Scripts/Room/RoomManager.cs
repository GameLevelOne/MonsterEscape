using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
	public RoomDataSO[] rooms;

	public RoomLoader roomLoader;
	public GameObject playerObj;
	public GameObject textGameOver;
	public GameObject btnRestart;
	public Fader fader;

	private RoomDataSO currRoom;
	private float xPos = 0f;
	private int currStackPos = 0;
	private Vector3 playerInitPos = new Vector3(-4.6f,-0.08f,0);

	void OnEnable(){
		Monster.DoGameOver += StartFadeOutGameOver;
		Fader.OnFadeOutGameOverFinished += AfterFadeOutGameOver;
	}

	void OnDisable (){
		Monster.DoGameOver -= StartFadeOutGameOver;
		Fader.OnFadeOutGameOverFinished -= AfterFadeOutGameOver;
	}

	void Start () {
		Init();
	}

	void Init(){
		playerObj.transform.localPosition = playerInitPos;
		currRoom = rooms[(int)RoomNames.Room6];
		roomLoader.Load(currRoom.roomName);
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

	void StartFadeOutGameOver (){
		fader.FadeOutGameOver();
	}

	void AfterFadeOutGameOver(){
		textGameOver.SetActive(true);
		btnRestart.SetActive(true);
		playerObj.GetComponent<Player> ().SetPause (true);
	}

	public void RestartGame ()
	{
		if (currRoom.roomName == RoomNames.Room8) {
			roomLoader.Unload(RoomNames.Room7);
			roomLoader.Unload(RoomNames.Room8);
			roomLoader.Unload(RoomNames.Room9);
		}
		fader.FadeIn();
		SceneManager.LoadScene("RoomRoot");
//		Init();
//		Camera.main.transform.localPosition = currRoom.cameraPos;
//		playerObj.transform.localPosition = playerInitPos;
//		playerObj.GetComponent<Player> ().SetPause (false);
//		textGameOver.SetActive(false);
//		btnRestart.SetActive(false);
//		playerObj.GetComponent<Player>().RestartPlayer();
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

		if (currRoom.roomName == RoomNames.Room2) {
			rooms[(int)RoomNames.Room11].stackPos=2;
		}
	}

	public void ChangeScene (RoomDataSO targetRoom,PortalType targetPortal)
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
		playerObj.transform.localPosition = currRoom.playerSpawnPos[(int)targetPortal];
		playerObj.GetComponent<Player> ().SetPause (false);
//		Debug.Log ("halooo");
		fader.FadeIn();
	}
}
