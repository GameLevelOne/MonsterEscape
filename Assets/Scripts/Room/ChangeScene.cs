using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tags{
	Portal,EnterAble
}

public class ChangeScene : MonoBehaviour {
	public RoomManager roomManager;
	public Fader fader;

	RoomDataSO targetRoom;
	PortalType targetPortal;
	bool isLoaded=false;
	Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}

	void OnEnable(){
		Fader.OnFadeOutFinished += ReadyToChangeScene;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= ReadyToChangeScene;
	}

	void ReadyToChangeScene (){
		isLoaded=false;
		roomManager.ChangeScene(targetRoom,targetPortal);
	}

	public void EnterAbleChangeScene(RoomDataSO targetRoom, PortalType portalType){
			this.targetRoom = targetRoom;
			this.targetPortal = portalType;
			fader.FadeOut ();

			player.SetPause (true);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == Tags.Portal.ToString ()) {
			if (!isLoaded) {
				//roomLoadController.AddRoom (roomName);
				//Spawn();
//				print("OBJECT YG DITABRAK"+col.gameObject.name);
				targetRoom = col.GetComponent<Portal> ().targetRoom;
//				print("ChangeScene TargetRoom: "+col.GetComponent<Portal> ().targetRoom);
				targetPortal = col.GetComponent<Portal> ().portalType;
				fader.FadeOut ();
				isLoaded = true;

				player.SetPause (true);
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
