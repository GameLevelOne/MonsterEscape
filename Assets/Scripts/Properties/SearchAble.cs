using UnityEngine.UI;
using UnityEngine;

public class SearchAble : Properties {
	const int MAX_ITEM = 5;

	[Header("Ko Elwin")]
	public ItemSO[] items;

	[Header("Reference")]
	public GameObject searchAbleInventory;
	public Transform Canvas;

	protected GameObject tempInventory;

	protected void Start(){
		//InitInventory();
	}

	protected void InitInventory(){
		tempInventory = Instantiate(searchAbleInventory) as GameObject;

		tempInventory.GetComponent<RectTransform>().SetParent(Canvas);
		tempInventory.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		tempInventory.GetComponent<RectTransform>().localScale = Vector2.one;

		tempInventory.GetComponent<SearchAbleInventory>().SetItems(items);
		tempInventory.GetComponent<SearchAbleInventory>().searchable = this;

		tempInventory.SetActive(false);
	}
		
	public void ShowSearchAbleInventory(){
		tempInventory.SetActive(true);

	}

	protected void HideSearchAbleInventory(){
		tempInventory.SetActive(false);
	}


}