using UnityEngine;

public class SearchAbleInventory : Inventory {
	Animator thisAnim;

	void Start(){
		UpdateInventory();
		thisAnim = GetComponent<Animator>();
	}

	public void SetItems(ItemSO[] item){
		items = item;
	}

	public void Show(){
		UpdateInventory();
		thisAnim.SetInteger("State",1);
	}

	void Hide(){
		thisAnim.SetInteger("State",0);
	}

	public void ButtonXOnClick(){
		Hide();
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
