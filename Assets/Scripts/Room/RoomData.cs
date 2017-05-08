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
	RoomLeft,
	RoomMiddle,
	RoomRight
}

public class RoomData : MonoBehaviour {
	public int stackPos = 0;
	public string[] adjacentRoomNames = new string[4]; //north,east,west,south
	public string sceneName;
	public Vector3 cameraPos;
	public Vector3 playerSpawnPos;
}
