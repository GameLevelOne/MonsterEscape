using UnityEngine;

public class MonsterRangeAttack : MonoBehaviour {

	Monster monster;

	void Start(){
		monster = transform.parent.GetComponent<Monster>();
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			monster.SetAnimState(MonsterAnimState.ATTACK);
		}
	}
}
