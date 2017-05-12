using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PortalType{
	TopLeft,BottomLeft,TopRight,BottomRight,Other
}

public class Portal : MonoBehaviour {
	public RoomDataSO targetRoom;
	public PortalType portalType;

}
