using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : Items {
	Animator thisAnim;

	void Awake()
	{
		thisAnim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Monster"){
			other.GetComponent<Monster>().GetStunned();
			thisAnim.SetTrigger("Explode");
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void Destroy()
	{
		Destroy(gameObject);
	}

}
