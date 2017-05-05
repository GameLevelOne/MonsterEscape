using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour {
	private bool readyToChangeScene = false;

	private AsyncOperation asop;

	void OnEnable(){
		Fader.OnFadeOutFinished += ReadyToChangeScene;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= ReadyToChangeScene;
	}

	public void Load (string roomName){
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		//SceneManager.LoadSceneAsync(roomName,LoadSceneMode.Single);
		asop = SceneManager.LoadSceneAsync (roomName, LoadSceneMode.Single);
		asop.allowSceneActivation = false;
		StartCoroutine(LoadScene(asop));

	}

	void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode){
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		//StartCoroutine(AfterLoad(scene));
	}

	public void Unload (string roomName){
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		SceneManager.UnloadSceneAsync(roomName);
	}

	void SceneManager_sceneUnloaded (Scene arg0)
	{
		SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
	}

	void ReadyToChangeScene (){
		asop.allowSceneActivation=true;
	}

	IEnumerator AfterLoad (Scene scene)
	{
		while (scene.isLoaded == false) {
			yield return new WaitForEndOfFrame();
		}
		//Unload("RoomLeft");
	}

	IEnumerator LoadScene (AsyncOperation asop)
	{

		while (asop.progress < 0.9f) {
			yield return null;
		}

	}
}
