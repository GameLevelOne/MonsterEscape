using UnityEngine;

public class SearchAbleInventory : Inventory {

	[HideInInspector]
	public SearchAble searchable;

	void Start(){
		InitInventory();
	}

	protected override void InitInventory(){
		UpdateInventory();
	}

	public void SetItems(ItemSO[] item){
		items = item;
	}

	public void ButtonXOnClick(){
		gameObject.SetActive(false);
		searchable.EnablePlayerControl();
	}
}
