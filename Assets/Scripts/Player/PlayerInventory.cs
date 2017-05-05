using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : Inventory {
	public Image highlight;

	const float startX = 50;
	static float gapX = 100;

	Animator anim;

	int highlightCursor;

	void Start(){
		InitInventory();
	}

	protected override void InitInventory(){
		anim = GetComponent<Animator>();
		DeselectItem();
		UpdateInventory();
	}

	public void Show(){ anim.SetBool("Show",true); }
	public void Hide(){ anim.SetBool("Show",false); }

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

	public override void RemoveItem(int index){
		if(highlightCursor == index) DeselectItem();
		items[index] = null;
		UpdateInventory();
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
	}
	void DeselectItem(){
		highlightCursor = -1;
		if(highlight.enabled) highlight.enabled = false;
	}
}
