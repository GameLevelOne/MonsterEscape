using UnityEngine.UI;
using UnityEngine;

public class SearchAble : Properties {
	[Header("Ko Elwin")]
	public ItemSO[] items = new ItemSO[5];

	[Header("Reference")]
	public SearchAbleInventory searchAbleInventory;

	Animator thisAnim;
		
	public void Search(){
		searchAbleInventory.Show();
	}



}