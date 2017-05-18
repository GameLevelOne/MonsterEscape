using UnityEngine;
using System.Collections.Generic;

public class SearchAble : Properties {
	[Header("Dynamic Reference")]
	public List<Items> items = new List<Items>();

	[Header("Absolute Reference")]
	public SearchAbleInventory searchAbleInventory;
		
	public void Search(PlayerInventory pi){
		searchAbleInventory.Show(items, pi);
	}
}