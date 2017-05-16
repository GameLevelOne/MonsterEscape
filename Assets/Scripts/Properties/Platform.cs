using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public void IgnorePlayer(bool ignore, BoxCollider2D playerCollider) {
		if (ignore) {
			Physics2D.IgnoreCollision(playerCollider, GetComponent<BoxCollider2D>());
		} else {
			Physics2D.IgnoreCollision(playerCollider, GetComponent<BoxCollider2D>(),false);
		}
	}

}
