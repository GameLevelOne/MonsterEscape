using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	public delegate void JoystickMovedEvent (JoystickDirection dir);
	public event JoystickMovedEvent OnJoystickMove;

	RectTransform bgImage;
	RectTransform handleImage;

	void Start()
	{
		bgImage = GetComponent<RectTransform> ();
		handleImage = transform.GetChild (0).GetComponent<RectTransform> ();
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImage, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImage.sizeDelta.x);
			pos.y = (pos.y / bgImage.sizeDelta.y);

//			float x = (bgImage.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
//			float y = (bgImage.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;
			float x = pos.x * 2;
			float y = pos.y * 2;

			Vector2 newDir = new Vector2 (x, y);
			newDir = (newDir.magnitude > 1f) ? newDir.normalized : newDir;
			JoystickDirection jDir = new JoystickDirection (newDir);

			handleImage.anchoredPosition = new Vector2 (newDir.x*(bgImage.sizeDelta.x/3),newDir.y*(bgImage.sizeDelta.y/3));

			if (OnJoystickMove != null)
				OnJoystickMove (jDir);
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		if (OnJoystickMove != null)
			OnJoystickMove (JoystickDirection.zero);
		handleImage.anchoredPosition = Vector2.zero;
	}
		
}
