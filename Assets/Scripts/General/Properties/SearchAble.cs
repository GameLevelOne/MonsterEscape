using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAble : Properties {
	const int MAX_ITEM = 5;

	public List<ItemSO> items = new List<ItemSO>();

	public void GetItem(ItemSO item){
		
	}

	public void SetItem(ItemSO item){
		if(items.Count >= 5){
			Debug.Log("FULL"); return;
		}

		items.Add(item);
	}


}
