using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsChecker : MonoBehaviour {

	public bool leftSide;
	public bool goingUp;
	public Stairs stairsBody;

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log ("Other: "+other.name);
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
//			Debug.Log ("Condition: "+enableHorizontal+","+enableVertical);
			if (enableHorizontal && enableVertical) {
				stairsBody.ActivateStairs(true,player.lowerPlayerCollider);
			} else {
				stairsBody.ActivateStairs(false,player.lowerPlayerCollider);
			}

		}

	}
}
