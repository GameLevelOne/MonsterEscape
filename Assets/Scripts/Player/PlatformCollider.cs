using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {

	public delegate void PlatformColliderEvent ();
	public event PlatformColliderEvent OnPlatformEnter;
	public event PlatformColliderEvent OnPlatformExit;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Heeeeee???");
		if (other.tag == "Platform")
			if (OnPlatformEnter != null)
				OnPlatformEnter ();
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Platform")
			if (OnPlatformExit != null)
				OnPlatformExit ();
	}


}
