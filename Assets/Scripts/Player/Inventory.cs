using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

	public Image[] itemImages;
	public Image highlight;

	Animator anim;
	ItemSO[] items = new ItemSO[5];

	int highlightCursor;

	const float startX = 50;
	static float gapX = 100;

	void Start(){
		InitInventory();
	}

	void InitInventory(){
		anim = GetComponent<Animator>();
		DeselectItem();
		UpdateInventory();
	}

	void UpdateInventory(){
		for(int i = 0;i<items.Length;i++){
			if(items[i] != null){
				itemImages[i].enabled = true;
				itemImages[i].sprite = items[i].itemSprite;
			}else{
				itemImages[i].enabled = false;
			}
		}
	}

	/// <summary>
	/// Show inventory.
	/// </summary>
	public void Show(){
		anim.SetBool("Show",true);
	}

	/// <summary>
	/// Hide inventory.
	/// </summary>
	public void Hide(){
		anim.SetBool("Show",false);
	}

	/// <summary>
	/// Set item to inventory, filling empty available slots. ignores if inventory is full
	/// </summary>
	public void SetItem(ItemSO item){
		for(int i = 0;i<items.Length;i++){
			if(items[i] != null){
				items[i] = item;
				UpdateInventory();
				return;
			}
		}
		Debug.Log("FULL");
	}

	public ItemSO GetItem(int index){
		return items[index];
	}


	/// <summary>
	/// Highlight selected item
	/// </summary>
	void SelectItem(int index){
		
		highlightCursor = index;
		highlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (gapX * index),0);
		highlight.enabled = true;
	}

	/// <summary>
	/// Unhighlight selected item
	/// </summary>
	void DeselectItem(){
		highlightCursor = -1;
		if(highlight.enabled) highlight.enabled = false;
	}
		
	/// <summary>
	/// Remove item from inventory
	/// </summary>
	/// <param name="index">Index.</param>
	public void RemoveItem(int index){
		if(highlightCursor == index) DeselectItem();
		items[index] = null;
		UpdateInventory();
	}

	/// <summary>
	/// EVENT TRIGGER ONLY!
	/// </summary>
	/// <param name="index">Index.</param>
	public void ItemOnPointerClick(int index){
		print("ASDASD");
		if(highlightCursor != index){
			SelectItem(index);
			highlightCursor = index;
		}else{
			DeselectItem();
		}
	}
}
