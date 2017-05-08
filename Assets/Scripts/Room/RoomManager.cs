using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour {
	public GameObject[] sceneObjects;

	public RoomLoader roomLoader;
	public GameObject playerObj;
	public Fader fader;

	//private GameObject currSceneObj;
	private string roomName = "room";
	private float xPos = 0f;
	private int currStackPos = 0;

	// Use this for initialization
	void Start () {
		Init();
	}

	void Init(){
		roomLoader.Load(RoomNames.RoomMiddle.ToString());
		roomLoader.Load(RoomNames.RoomLeft.ToString());
		roomLoader.Load(RoomNames.RoomRight.ToString());

		sceneObjects[0].GetComponent<RoomData>().stackPos=1;
		sceneObjects[1].GetComponent<RoomData>().stackPos=0;
		sceneObjects[2].GetComponent<RoomData>().stackPos=1;
	}

	public void RemoveRoom (string roomName){
		roomLoader.Unload(roomName);
	}

	public void CheckUnload(){
		for (int i = 0; i < sceneObjects.Length; i++) {
			RoomData temp = sceneObjects [i].GetComponent<RoomData> ();
			if (temp.stackPos == 2) {
				roomLoader.Unload (temp.sceneName);
			}
			Debug.Log("i="+i+", stackPos:"+temp.stackPos);
		}
	}

	public void SetStackPos(int currSceneIdx){
		for (int i = 0; i < sceneObjects.Length; i++) {
			RoomData temp = sceneObjects [i].GetComponent<RoomData> ();
			if (i != currSceneIdx) {
				if (temp.stackPos < 2)
					temp.stackPos++;
				else
					temp.stackPos = 2;
			} else {
				temp.stackPos = 0;
			}
		}
	}

	public void ChangeScene (RoomNames targetRoom)
	{
		if (targetRoom == RoomNames.RoomLeft) {
			SetStackPos (0);
		} else if (targetRoom == RoomNames.RoomMiddle) {
			SetStackPos (1);
			if (sceneObjects [(int)RoomNames.RoomLeft].GetComponent<RoomData> ().stackPos == 2) {
				roomLoader.Load (RoomNames.RoomLeft.ToString ());
			} else if (sceneObjects [(int)RoomNames.RoomRight].GetComponent<RoomData> ().stackPos == 2) {
				roomLoader.Load(RoomNames.RoomRight.ToString());
			}

		} else if (targetRoom == RoomNames.RoomRight) {
			SetStackPos(2);
		}
		CheckUnload();
		Camera.main.transform.localPosition = sceneObjects [(int)targetRoom].GetComponent<RoomData> ().cameraPos;
		playerObj.transform.localPosition = sceneObjects [(int)targetRoom].GetComponent<RoomData> ().playerSpawnPos;
		fader.FadeIn();
	}
}
