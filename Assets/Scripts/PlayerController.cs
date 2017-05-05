using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float walkSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * walkSpeed);
			transform.localScale = new Vector3(-1,1,0);
		} else if(Input.GetKey(KeyCode.D)){
			transform.Translate(Vector3.right * walkSpeed);
			transform.localScale = new Vector3(1,1,0);
		}
	}
}
