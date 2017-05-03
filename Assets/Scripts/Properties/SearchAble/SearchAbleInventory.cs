using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAbleInventory : Inventory {

	void Start(){
		InitInventory();
	}

	protected override void InitInventory(){
		UpdateInventory();
	}

}
