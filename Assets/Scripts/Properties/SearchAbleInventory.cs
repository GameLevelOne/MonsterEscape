using UnityEngine;

public class SearchAbleInventory : Inventory {

	void Start(){
		InitInventory();
	}

	protected override void InitInventory(){
		UpdateInventory();
	}

	public void SetItems(ItemSO[] item){
		items = item;
	}

}
