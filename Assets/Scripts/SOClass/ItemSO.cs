using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{ FOOD, TRAP }

[CreateAssetMenu(fileName = "Item_",menuName = "Cards/Item", order = 1)]
public class ItemSO : ScriptableObject {
	public string itemName = "NewItem";
	public Sprite itemSprite = null;
	public ItemType itemType;
}
