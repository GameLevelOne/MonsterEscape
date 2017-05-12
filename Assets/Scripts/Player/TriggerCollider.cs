using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour {

	public delegate void TriggerColliderEvent (GameObject other);
	public event TriggerColliderEvent OnTriggerEnter;
	public event TriggerColliderEvent OnTriggerExit;
	public string tagCheck;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == tagCheck)
		if (OnTriggerEnter != null)
			OnTriggerEnter (other.gameObject);
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == tagCheck)
		if (OnTriggerExit != null)
			OnTriggerExit (other.gameObject);
	}


}
