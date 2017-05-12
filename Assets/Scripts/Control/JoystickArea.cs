using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public Joystick joystick;

	public virtual void OnPointerDown(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		RectTransform rt = GetComponent<RectTransform> ();
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (rt , ped.position, ped.pressEventCamera, out pos)) {
//			pos.x = (pos.x / (Screen.width/2));
//			pos.y = (pos.y / (Screen.height/2));
			joystick.gameObject.SetActive (true);
			joystick.GetComponent<RectTransform> ().anchoredPosition = pos;
			joystick.OnDrag (ped);
		}
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		joystick.gameObject.gameObject.SetActive (false);
	}
}
