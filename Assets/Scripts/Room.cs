using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomDirection{
	NORTH,
	EAST,
	WEST,
	SOUTH
}

public enum RoomNames{
	RoomMiddle,
	RoomLeft,
	RoomRight
}

public class Room : MonoBehaviour {
	public int stackPos = 0;
	public int adjacentRoomCount = 0;
	public string[] adjacentRoomNames = new string[4];


	// Use this for initialization
	void Start () {

	}

	public void SetRoomData (string toNorth, string toEast, string toWest, string toSouth)
	{
		adjacentRoomNames [0] = toNorth;
		adjacentRoomNames [1] = toEast;
		adjacentRoomNames [2] = toWest;
		adjacentRoomNames [3] = toSouth;

		for (int i = 0; i < 4; i++) {
			if (!string.IsNullOrEmpty (adjacentRoomNames [i])) {
				adjacentRoomCount++;
			}
		}
	}

	public void SetStackPos (int stackPos){
		this.stackPos=stackPos;
	}

	string GetRoomData (int roomIdx){
		return adjacentRoomNames[roomIdx];
	}

}
