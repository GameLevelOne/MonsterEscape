using UnityEngine;

public class MonsterRangePatrol : MonoBehaviour {

	Monster monster;

	void Start(){
		monster = transform.parent.GetComponent<Monster>();
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
//			monster.SetAnimState(MonsterAnimState.RUN);
			GetComponent<CircleCollider2D>().radius = 10;
		}
	}


	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
//			if(monster.State == MonsterAnimState.RUN){
//				monster.SetAnimState(MonsterAnimState.CONFUSED);
//				GetComponent<CircleCollider2D>().radius = 7;
//			}
		}
	}
}
