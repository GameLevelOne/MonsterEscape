using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAble : MonoBehaviour {

	public Transform climbPos;

	public void SetPlayerLocation (Transform pt)
	{
		pt.position = climbPos.position;
	}
}
