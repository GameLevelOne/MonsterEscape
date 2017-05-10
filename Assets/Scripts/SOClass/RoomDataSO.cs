using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomNames{
//	Room1,Room2,Room3,Room4,Room5,Room6,Room7,Room8,Room9,Room10,Room11,Room12,NULL -> original
	Room1,Room2,Room3,Room4,Room5,Room6Temp,Room7Temp,Room8,Room9,Room10,Room11,Room12,NULL // ->temp
}

[CreateAssetMenu(fileName = "RoomData_",menuName = "Cards/RoomData", order = 2)]
public class RoomDataSO : ScriptableObject {
	public int stackPos = 0;
	public RoomNames[] adjacentRoomNames = new RoomNames[4]; //clockwise starts from right
	public RoomNames roomName;
	public Vector3[] playerSpawnPos = new Vector3[4];
	public Vector3 cameraPos;

}
