using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAble : Properties {
	const int MAX_ITEM = 5;

	public List<ItemSO> items = new List<ItemSO>();

	void OpenSearchUI(){
		//show Inventory UI
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			//register OnActionButton
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			//unregister event OnActionButton
		}
	}
}
