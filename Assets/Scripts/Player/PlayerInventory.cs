using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : Inventory {
	public Player player;
	public Image highlight;
	
	const float startX = 50;
	static float gapX = 100;

	Animator anim;
	GameObject tempItem;

	int highlightCursor;
	int animState;

	void Start(){
		InitInventory();
	}

	protected override void InitInventory(){
		anim = GetComponent<Animator>();
		DeselectItem();
		RefreshInventory();
	}

	public void ButtonInventoryOnClick(){
		if(animState == 1) animState = 0;
		else if(animState == 0)animState = 1;
		anim.SetInteger("State",animState);
	}

	public void SetItem(ItemSO item){
		for(int i = 0;i<items.Length;i++){
			if(items[i] == null){
				items[i] = item;
				RefreshInventory();
				return;
			}
		}
		Debug.Log("FULL");
	}

	public override void RemoveItem(int index){
		if(highlightCursor == index) DeselectItem();
		items[index] = null;
		RefreshInventory();
	}

	//EVENT TRIGGER
	public void ItemOnPointerClick(int index){
		if(highlightCursor != index){
			SelectItem(index);
			highlightCursor = index;
		}else{
			DeselectItem();
		}
	}

	void SelectItem(int index){
		highlightCursor = index;
		highlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (gapX * index),0);
		highlight.enabled = true;

		//item
		player.HoldItem(items[index],index);
	}

	void DeselectItem(){
		highlightCursor = -1;
		if(highlight.enabled) highlight.enabled = false;

		//item
		Destroy(tempItem);
		tempItem = null;
		player.placeTrapButton.SetActive(false);
	}
}
