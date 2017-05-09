using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
	public RoomDataSO targetRoom;

	private RoomManager roomManager;
	private RoomLoader roomLoader;
	private Fader fader;
	private GameObject playerObj;

	private string roomManagerTag = "RoomManager";
	private string roomLoaderTag = "RoomLoader";
	private string playerTag = "Player";
	private string faderTag = "Fader";
	private bool isLoaded = false;

	void Start(){
		roomManager = GameObject.FindGameObjectWithTag(roomManagerTag).GetComponent<RoomManager>();
		roomLoader = GameObject.FindGameObjectWithTag(roomLoaderTag).GetComponent<RoomLoader>();
		fader = GameObject.FindGameObjectWithTag(faderTag).GetComponent<Fader>();
		playerObj = GameObject.FindGameObjectWithTag(playerTag);
	}

//	void OnTriggerEnter2D (Collider2D col)
//	{
//		if (col.tag == "Player") {
//			if (!isLoaded) {
//				//roomLoadController.AddRoom (roomName);
//				//Spawn();
//				fader.FadeOut();
//				isLoaded=true;
//			}
//		}
//	}
//
//	void OnTriggerExit2D (Collider2D col)
//	{
//		if (col.tag == "Player") {
//			isLoaded=false;
//		}
//	}
}
