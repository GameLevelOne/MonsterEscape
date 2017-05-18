using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTitleManager : MonoBehaviour {
	public Fader fader;

	// Use this for initialization
	void Start () {
		fader.FadeIn();
	}
	
	void OnEnable(){
		Fader.OnFadeOutFinished += ReadyToChangeScene;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= ReadyToChangeScene;
	}

	void ReadyToChangeScene(){
		SceneManager.LoadScene("RoomRoot");
	}

	public void OnPanelClick (){
		fader.FadeOut();
	}
}
