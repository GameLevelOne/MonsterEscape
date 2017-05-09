using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float walkSpeed = 0.1f;

	bool isLoaded=false;
	Fader fader;
	RoomManager roomManager;
	string faderTag = "Fader";
	string roomManagerTag = "RoomManager";
	RoomDataSO targetRoom;

	// Use this for initialization
	void Start () {
		fader = GameObject.FindGameObjectWithTag(faderTag).GetComponent<Fader>();
		roomManager = GameObject.FindGameObjectWithTag(roomManagerTag).GetComponent<RoomManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * walkSpeed);
			transform.localScale = new Vector3(-1,1,0);
		} else if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector3.right * walkSpeed);
			transform.localScale = new Vector3(1,1,0);
		}
	}

	void OnEnable(){
		Fader.OnFadeOutFinished += ReadyToChangeScene;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= ReadyToChangeScene;
	}

	void ReadyToChangeScene (){
		isLoaded=false;
		roomManager.ChangeScene(targetRoom);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Portal") {
			if (!isLoaded) {
				//roomLoadController.AddRoom (roomName);
				//Spawn();
				targetRoom = col.GetComponent<Portal>().targetRoom;
				fader.FadeOut();
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
