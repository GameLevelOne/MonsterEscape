using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAble : Properties {
	
	public delegate void Hide();
	public delegate void Expose();
	public event Hide OnHide;
	public event Expose OnExpose;

	bool bEmpty;


}
