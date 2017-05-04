using UnityEngine;
public class MainCanvas : MonoBehaviour {
	static MainCanvas instance = null;
	public static MainCanvas Instance{ get{ return instance;} }
	void Awake(){ instance = this; }
}