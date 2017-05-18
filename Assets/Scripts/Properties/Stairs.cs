using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	public BoxCollider2D[] disableOtherObjects;
	PolygonCollider2D thisCollider;

	void Start() {
		thisCollider = GetComponent<PolygonCollider2D>();
	}


	public void ActivateStairs (bool active, Collider2D playerCollider) {
		for (int i=0;i<disableOtherObjects.Length;i++) {
//			Debug.Log("Activate Stairs: "+active+", collider: "+playerCollider.name);
			Physics2D.IgnoreCollision(playerCollider, disableOtherObjects[i],active);
		}
		thisCollider.enabled = active;
	}
}
