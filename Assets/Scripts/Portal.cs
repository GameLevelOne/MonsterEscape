using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {
	public RoomLoader roomLoader;
	public Fader fader;
	public string roomName;

	private bool isLoaded = false;

	public delegate void SpawnPlayer();

	public static event SpawnPlayer Spawn;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			if (!isLoaded) {
				//roomLoadController.AddRoom (roomName);
				//Spawn();
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
