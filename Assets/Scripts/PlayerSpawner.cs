using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		DoSpawnPlayer();
	}
	
	void OnEnable(){
		Portal.Spawn += DoSpawnPlayer;
	}

	void OnDisable(){
		Portal.Spawn -= DoSpawnPlayer;
	}

	void DoSpawnPlayer (){
		playerPrefab = Instantiate(playerPrefab) as GameObject;
		playerPrefab.transform.localPosition = transform.localPosition;
	}
}
