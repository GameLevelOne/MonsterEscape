using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAble : Properties {

	public delegate void Enter();
	public event Enter OnEnter;

}
