using UnityEngine;

public class SearchAbleInventory : Inventory {
	Animator thisAnim;

	public GameObject panelItem, panelItemEmpty;

	PlayerInventory playerInventory;

	void Start()
	{
		
		thisAnim = GetComponent<Animator>();
	}

	public void Show(ItemSO[] items, PlayerInventory pi)
	{
		playerInventory = pi;
//		RefreshInventory();
		if(items.Length == 0){
			panelItemEmpty.SetActive(true);
			panelItem.SetActive(false);
		}else{
			this.items = items;
			RefreshInventory();
			panelItemEmpty.SetActive(false);
			panelItem.SetActive(true);
		}
		thisAnim.SetInteger("State",1);
	}

//	bool hasItem(){
//		for(int i = 0;i<items.Length;i++){
//			print(items[i]);
//			if(items[i] != null){
//				return true;
//			}
//		}
//		return false;
//	}

	void Hide()
	{
//		for(int i = 0;i<this.items.Length;i++) items[i] = null;
		thisAnim.SetInteger("State",0);
	}

	public void ButtonXOnClick()
	{
		Hide();
		playerInventory.ButtonInventoryOnClick();
	}

	public void ButtonItemOnClick(int index)
	{
		if(items[index] != null){
			playerInventory.SetItem(items[index]);
			items[index] = null;
			RefreshInventory();
		}
	}
}
