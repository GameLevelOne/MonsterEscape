using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {

	public delegate void PlatformColliderEvent ();
	public event PlatformColliderEvent OnPlatformEnter;
	public event PlatformColliderEvent OnPlatformExit;
	public string tagCheck;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == tagCheck)
			if (OnPlatformEnter != null)
				OnPlatformEnter ();
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == tagCheck)
			if (OnPlatformExit != null)
				OnPlatformExit ();
	}


}
