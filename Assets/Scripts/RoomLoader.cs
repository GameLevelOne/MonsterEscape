using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour {
	public Fader fader;
	private bool readyToChangeScene = false;

	private AsyncOperation asop;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

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
		StartCoroutine(AfterLoad(scene));
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
		Debug.Log("change");
		asop.allowSceneActivation=true;
	}

	IEnumerator AfterLoad (Scene scene)
	{
		while (scene.isLoaded == false) {
			yield return new WaitForEndOfFrame();
		}
		fader.FadeIn();
		Debug.Log("done");
		//Unload("RoomLeft");
	}

	IEnumerator LoadScene (AsyncOperation asop)
	{
		Debug.Log ("start loading");

		while (asop.progress < 0.9f) {
			Debug.Log(asop.progress.ToString());
			yield return null;
		}

	}
}
