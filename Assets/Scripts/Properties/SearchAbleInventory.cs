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
		Hide();
	}

	public void Show(){
		UpdateInventory();
		gameObject.SetActive(true);
	}

	void Hide(){
		gameObject.SetActive(false);
	}

	public void ButtonItemOnClick(int index){
		if(items[index] != null){
			//take the item selected and move it to player inventory
			//empty the slot
			items[index] = null;
			UpdateInventory();
		}
	}
}
