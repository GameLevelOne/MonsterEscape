using UnityEngine.UI;
using UnityEngine;

public enum PlayerMoveSide{ LEFT, RIGHT }

public class MoveAble : Properties {
	[HideInInspector] public PlayerMoveSide playerPullPos;
	[HideInInspector] public Transform player;

	float distance;
	bool isHolding;

	void Start(){
		Init();
	}

	void Init(){
		isHolding = false;
	}

	void Update(){
		if(isHolding){
			if(player != null){
				float tempX = playerPullPos == PlayerMoveSide.LEFT ? (player.position.x + distance) : (player.position.x - distance);
				transform.position = new Vector3(tempX,player.position.y,player.position.z);
			}
		}
	}

	public void OnPlayerAction(){
		isHolding = !isHolding;
		if(isHolding){
			distance = Mathf.Abs(transform.position.x - player.position.x);
		}
	}
}
