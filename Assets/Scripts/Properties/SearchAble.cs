using UnityEngine.UI;
using UnityEngine;

public class SearchAble : Properties {
	const int MAX_ITEM = 5;

	public ItemSO[] items;
	public GameObject searchAbleInventory;
	public Transform Canvas;
	GameObject tempInventory;
	Player player;

	void Start(){
		InitInventory();
	}

	void InitInventory(){
		tempInventory = Instantiate(searchAbleInventory) as GameObject;

		tempInventory.GetComponent<RectTransform>().SetParent(Canvas);
		tempInventory.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		tempInventory.GetComponent<RectTransform>().localScale = Vector2.one;

		tempInventory.GetComponent<SearchAbleInventory>().SetItems(items);
		tempInventory.GetComponent<SearchAbleInventory>().searchable = this;

		tempInventory.SetActive(false);
	}

	//OnActionButton
	void ShowSearchAbleInventory(){
		tempInventory.SetActive(true);
		//disable player control
	}

	public void EnablePlayerControl(){
		//enable player control
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			//register OnActionButton
			player = p.GetComponent<Player>();
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			//unregister event OnActionButton
			player = null;
		}
	}
}