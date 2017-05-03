﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void Load (string roomName){
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		SceneManager.LoadSceneAsync(roomName,LoadSceneMode.Additive);
	}

	void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode){
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		StartCoroutine(AfterLoad(scene));
	}

	IEnumerator AfterLoad (Scene scene)
	{
		while (scene.isLoaded == false) {
			yield return new WaitForEndOfFrame();
		}

		Debug.Log("done");
	}
}
