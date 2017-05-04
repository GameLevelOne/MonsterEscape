using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState {
	PLAYER_IDLE,
	PLAYER_WALK,
	PLAYER_FALL,
	PLAYER_CLIMB,
	PLAYER_CRAWL,
	PLAYER_EAT,
	PLAYER_CARRY,
	PLAYER_OPERATE,
	PLAYER_HIDE,
	PLAYER_DAMAGE
}


public class Player : MonoBehaviour {

	Transform playerTransform;
	Animator playerAnim;
	SpriteRenderer playerSprite;

	public PlatformCollider feetCollider;

	public Joystick joystick;

	public float speed = 10f;
	JoystickDirection dir;
	bool fallFlag = true;
	PlayerState playerAction = PlayerState.PLAYER_IDLE;

	void Start () {
		playerTransform = GetComponent<Transform> ();
		playerAnim = GetComponent<Animator> ();
		playerSprite = GetComponent<SpriteRenderer> ();

		dir = JoystickDirection.zero;
		joystick.OnJoystickMove += OnDirChange;
		feetCollider.OnPlatformEnter += OnFeetPlatformEnter;
		feetCollider.OnPlatformExit += OnFeetPlatformExit;
	}

	void Update () 
	{
		Vector2 curPos = playerTransform.localPosition;

		//Eat
		if (playerAction >= PlayerState.PLAYER_EAT) {
			AnimChange (playerAction);
		} else {

			//Walk Horizontally
			if (dir.horizontal != Vector2.zero) {
				if (fallFlag) {
					//Falling
					AnimChange (PlayerState.PLAYER_FALL);
				} else {
					AnimChange (PlayerState.PLAYER_WALK, 0.5f + (Mathf.Abs (dir.horizontal.x) / 2));	
				}
				playerTransform.localPosition = curPos + (dir.horizontal * speed);
			} else {
				if (fallFlag) {
					//Falling
					AnimChange (PlayerState.PLAYER_FALL);
				} else {
					AnimChange (PlayerState.PLAYER_IDLE);	
				}
			}

			//Flip Facing
			if (dir.IsLeft) {
				playerSprite.flipX = true;
			} else if (dir.IsRight) {
				playerSprite.flipX = false;
			}				
		}
	}

	public void PlayerAction(int state)
	{
		playerAction = (PlayerState)state;
	}


	void OnDirChange(JoystickDirection newDir)
	{
		dir = newDir;	
	}

	void OnFeetPlatformEnter()
	{
		fallFlag = false;
	}
	void OnFeetPlatformExit()
	{
		fallFlag = true;
	}

	void OnDestroy()
	{
		joystick.OnJoystickMove -= OnDirChange;
		feetCollider.OnPlatformEnter -= OnFeetPlatformEnter;
		feetCollider.OnPlatformExit -= OnFeetPlatformExit;
	}

	void AnimChange(PlayerState ps, float animSpeed = 1f)
	{
		playerAnim.SetInteger ("AnimState",(int)ps);
		playerAnim.speed = animSpeed;
	}
}
