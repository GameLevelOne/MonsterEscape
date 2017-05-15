using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	public BoxCollider2D[] disableOtherObjects;
	PolygonCollider2D thisCollider;

	void Start() {
		thisCollider = GetComponent<PolygonCollider2D>();
	}


	public void ActivateStairs (bool active) {
		for (int i=0;i<disableOtherObjects.Length;i++) {
			disableOtherObjects[i].enabled = !active;
		}
		thisCollider.enabled = active;
	}
}
