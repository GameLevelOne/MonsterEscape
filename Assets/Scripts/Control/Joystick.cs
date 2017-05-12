using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	public delegate void JoystickMovedEvent (JoystickDirection dir);
	public event JoystickMovedEvent OnJoystickMove;

	public float ignoreThreshold;
	public RectTransform bgImage;
	public RectTransform handleImage;
	RectTransform thisTransform;

	void Start()
	{
		thisTransform = GetComponent<RectTransform> ();
		bgImage.gameObject.SetActive (false);
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImage, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImage.sizeDelta.x);
			pos.y = (pos.y / bgImage.sizeDelta.y);

			float x = pos.x * 2;
			float y = pos.y * 2;

			Vector2 newDir = new Vector2 (x, y);
			newDir = (newDir.magnitude > 1f) ? newDir.normalized : newDir;
			handleImage.anchoredPosition = new Vector2 (newDir.x*(bgImage.sizeDelta.x/3),newDir.y*(bgImage.sizeDelta.y/3));

			Vector2 forDirection = new Vector2 ((Mathf.Abs (newDir.x) >= ignoreThreshold ? newDir.x : 0f), (Mathf.Abs (newDir.y) >= ignoreThreshold ? newDir.y : 0f));
			JoystickDirection jDir = new JoystickDirection (forDirection);


			if (OnJoystickMove != null)
				OnJoystickMove (jDir);
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		bgImage.gameObject.SetActive (true);
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (thisTransform, ped.position, ped.pressEventCamera, out pos)) {
			bgImage.anchoredPosition = pos;
		}
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		if (OnJoystickMove != null)
			OnJoystickMove (JoystickDirection.zero);
		handleImage.anchoredPosition = Vector2.zero;
		bgImage.gameObject.SetActive (false);
	}
		
}
