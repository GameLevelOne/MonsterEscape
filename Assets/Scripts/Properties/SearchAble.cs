using UnityEngine;
using UnityEngine.UI;

public class SearchAble : Properties {
	const int MAX_ITEM = 5;

	public ItemSO[] items;
	public GameObject searchAbleInventory;
	GameObject temp;

	void Start(){
		searchAbleInventory.GetComponent<SearchAbleInventory>().SetItems(items);
	}

	/// <summary>
	/// Register this method to player action event.
	/// </summary>
	void ShowSearchAbleInventory(){
		//show Inventory UI
		temp = Instantiate(searchAbleInventory) as GameObject;
		temp.GetComponent<RectTransform>().SetParent(MainCanvas.Instance.transform);
		temp.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		temp.GetComponent<RectTransform>().localScale = Vector2.one;
	}

	public void HideSearchAbleInventory(){
		Destroy(temp.gameObject);
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			//register OnActionButton
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			//unregister event OnActionButton
		}
	}
}