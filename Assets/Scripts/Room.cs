using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomDirection{
	NORTH,
	EAST,
	SOUTH,
	WEST
}

public class Room : MonoBehaviour {
	public int stackPos = 0;
	public string[] adjacentRoomNames = new string[4];


	// Use this for initialization
	void Start () {
		
	}

	public void SetRoomData (string toNorth, string toEast, string toSouth, string toWest){
		
	}

	public void SetStackPos (int stackPos){
		this.stackPos=stackPos;
	}

}
