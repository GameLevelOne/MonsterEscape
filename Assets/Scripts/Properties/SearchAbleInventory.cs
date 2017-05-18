using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SearchAbleInventory : Inventory {
	public GameObject panelItem, panelItemEmpty;

	Animator thisAnim;
	PlayerInventory playerInventory;

	void Start()
	{
		thisAnim = GetComponent<Animator>();
	}

	protected override void RefreshInventory()
	{
		if(items.Count == 0){
			for(int i = 0;i<itemImages.Length;i++) itemImages[i].sprite = null;
			panelItemEmpty.SetActive(true);
			panelItem.SetActive(false);
		}else{
			for(int i = 0;i<items.Count;i++) itemImages[i].sprite = items[i].itemData.itemSprite;
			panelItemEmpty.SetActive(false);
			panelItem.SetActive(true);
		}
	}

	public void Show(List<Items> items, PlayerInventory pi)
	{
		playerInventory = pi;
		this.items = items;
		RefreshInventory();
		thisAnim.SetInteger("State",1);
	}

	public void Hide()
	{
		thisAnim.SetInteger("State",0);
	}

	public override void ButtonItemOnClick(int index)
	{
		if(playerInventory.items.Count < MAX_PLAYER_ITEM_HOLD){
			playerInventory.Obtain(items[index]);
			Destroy(items[index].gameObject);
			items.RemoveAt(index);
			RefreshInventory();
		}else{
			Debug.Log("Player Inventory FULL");
		}
	}
}
