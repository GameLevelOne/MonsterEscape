using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public ItemSO[] items = new ItemSO[5];

	[Header("Reference")]
	public Image[] itemImages;

	protected virtual void InitInventory(){}

	protected void UpdateInventory(){
		for(int i = 0;i<items.Length;i++){
			if(items[i] != null){
				itemImages[i].enabled = true;
				itemImages[i].sprite = items[i].itemSprite;
			}else{
				itemImages[i].enabled = false;
			}
		}
	}

	public ItemSO GetItem(int index){
		return items[index];
	}

	public virtual void RemoveItem(int index){}


}
