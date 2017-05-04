using UnityEngine;

public class MonsterRangePatrol : MonoBehaviour {

	Monster monster;

	void Start(){
		monster = transform.parent.GetComponent<Monster>();
	}

	void OnTriggerEnter2D(Collider2D p){
		if(p.tag == "Player"){
			monster.SetAnimState(AnimState.RUN);
			GetComponent<CircleCollider2D>().radius = 10;
		}
	}

	void OnTriggerExit2D(Collider2D p){
		if(p.tag == "Player"){
			if(monster.State == AnimState.RUN){
				monster.SetAnimState(AnimState.CONFUSED);
				GetComponent<CircleCollider2D>().radius = 7;
			}
		}
	}
}
