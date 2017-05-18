using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : Inventory {
	public Player player;
	public Image highlight;
	
	const float startX = 50;
	static float gapX = 100;

	Animator anim;

	int highlightCursor = -1;
	bool animState = false;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	protected override void RefreshInventory()
	{
		for(int i = 0;i<MAX_PLAYER_ITEM_HOLD;i++) itemImages[i].sprite = null;

		if(items.Count == 0) for(int i = 0;i<MAX_PLAYER_ITEM_HOLD;i++) itemImages[i].gameObject.GetComponent<Button>().interactable = false;
		else{
			for(int i = 0;i<items.Count;i++){
				itemImages[i].sprite = items[i].itemData.itemSprite;
				itemImages[i].GetComponent<Button>().interactable = true;
			}
		}
	}

	public void ButtonInventoryOnClick()
	{
		animState = !animState;
		anim.SetInteger("State",animState ? 1 : 0);
	}

	public void Obtain(Items item)
	{
		if(items.Count >= MAX_PLAYER_ITEM_HOLD) Debug.Log("FULL");
		else{
			item.transform.SetParent(player.ItemsToHoldObject.transform,false);
			items.Add(item);
			RefreshInventory();
		}
	}

	public void UseItem()
	{
		items.RemoveAt(highlightCursor);
		UnHighlightItem();
		RefreshInventory();
	}

	void HighlightItem()
	{
		print("ASDASDSA");
		highlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (gapX * highlightCursor),0);
		highlight.enabled = true;

		player.HoldItem(items[highlightCursor]);
	}

	void UnHighlightItem()
	{
		if(highlight.enabled) highlight.enabled = false;
		player.CancelHoldItem();
		highlightCursor = -1;
	}
		

	//Button
	public override void ButtonItemOnClick(int index)
	{
		print(index);
		if(highlightCursor != index){
			highlightCursor = index;
			HighlightItem();
		}else{
			UnHighlightItem();
		}
	}
}