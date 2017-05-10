using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour {
	private bool readyToChangeScene = false;

	public void Load (RoomNames roomName){
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		//SceneManager.LoadSceneAsync(roomName,LoadSceneMode.Single);
		SceneManager.LoadSceneAsync (roomName.ToString(), LoadSceneMode.Additive);
	}

	void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode){
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		//StartCoroutine(AfterLoad(scene));
	}

	public void Unload (RoomNames roomName){
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		SceneManager.UnloadSceneAsync(roomName.ToString());
	}

	void SceneManager_sceneUnloaded (Scene arg0)
	{
		SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
	}

	IEnumerator AfterLoad (Scene scene)
	{
		while (scene.isLoaded == false) {
			yield return new WaitForEndOfFrame();
		}
		//Unload("RoomLeft");
	}

}
