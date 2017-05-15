using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsChecker : MonoBehaviour {

	public bool leftSide;
	public bool goingUp;
	public PolygonCollider2D stairsBody;

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			Player player = other.transform.parent.parent.GetComponent<Player> (); 
			bool enableHorizontal = false;
			if (((!leftSide) && (player.playerDir.IsRight))
			    || ((leftSide) && (player.playerDir.IsLeft))) {
				enableHorizontal = true;
			}
			bool enableVertical = false;
			if (((!goingUp) && (player.playerDir.IsDown))
				|| ((goingUp) && (player.playerDir.IsUp))) {
				enableVertical = true;
			}
			Debug.Log ("Condition: "+enableHorizontal+","+enableVertical);
			if (enableHorizontal && enableVertical) {
				stairsBody.enabled = true;
			} else {
				stairsBody.enabled = false;
			}

		}

	}
}
