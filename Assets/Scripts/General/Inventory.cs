using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	protected const int MAX_PLAYER_ITEM_HOLD = 5;
	public List<Items> items = new List<Items>();
	public Image[] itemImages;

	protected virtual void RefreshInventory(){}
	public virtual void ButtonItemOnClick(int index){}
}
