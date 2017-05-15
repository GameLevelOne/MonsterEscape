using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : EventTrigger {
	public delegate void ActionEvent (PlayerState state);
	public event ActionEvent OnActionDown;
	public event ActionEvent OnActionUp;

	Text actionText;

	PlayerState activeState;


	void Start () {
		gameObject.SetActive (false);	
		activeState = PlayerState.PLAYER_IDLE;
		actionText = transform.GetChild (0).GetComponent<Text> ();
	}

	public void Activate(PlayerState newState, string text) {
		gameObject.SetActive (true);
		actionText.text = text;
		activeState = newState;
	}
	public void Deactivate() {
		gameObject.SetActive (false);
	}


	public override void OnPointerDown(PointerEventData data) {
		if (OnActionDown != null)
			OnActionDown (activeState);
	}
	public override void OnPointerUp(PointerEventData data) {
		if (OnActionUp != null)
			OnActionUp (activeState);
	}
}
