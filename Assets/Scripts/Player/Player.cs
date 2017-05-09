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
	Rigidbody2D playerRigidBody;

	public PlatformCollider feetCollider;
	public PlatformCollider ladderCollider;
	public PlatformCollider upperCrawlCollider;
	public PlatformCollider lowerCrawlCollider;

	public Joystick joystick;

	public float speed = 10f;
	JoystickDirection dir;
	bool fallFlag = true;
	bool ladderFlag = false;
	public bool crawlFlag = false;
	public bool upperCrawlFlag = false;
	public bool lowerCrawlFlag = false;
	PlayerState playerAction = PlayerState.PLAYER_IDLE;

	void Start () {
		playerTransform = GetComponent<Transform> ();
		playerAnim = GetComponent<Animator> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerRigidBody = GetComponent<Rigidbody2D> ();

		dir = JoystickDirection.zero;
		joystick.OnJoystickMove += OnDirChange;
		feetCollider.OnPlatformEnter += OnFeetPlatformEnter;
		feetCollider.OnPlatformExit += OnFeetPlatformExit;
		ladderCollider.OnPlatformEnter += OnLadderEnter;
		ladderCollider.OnPlatformExit += OnLadderExit;
		upperCrawlCollider.OnPlatformEnter += OnUpperCrawlEnter;
		upperCrawlCollider.OnPlatformExit += OnUpperCrawlExit;
		lowerCrawlCollider.OnPlatformEnter += OnLowerCrawlEnter;
		lowerCrawlCollider.OnPlatformExit += OnLowerCrawlExit;
	}

	void FixedUpdate () 
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
					if (crawlFlag) {
						AnimChange (PlayerState.PLAYER_CRAWL, 0.5f + (Mathf.Abs (dir.horizontal.x) / 2));	
					} else {
						AnimChange (PlayerState.PLAYER_WALK, 0.5f + (Mathf.Abs (dir.horizontal.x) / 2));	
					}						
				}
				if ((!fallFlag) || (!ladderFlag)) {
					playerTransform.localPosition = curPos + (dir.horizontal * speed);
				}
			} else {
				if (fallFlag) {
					//Falling
					AnimChange (PlayerState.PLAYER_FALL);
				} else {
					if (crawlFlag) {
						AnimChange (PlayerState.PLAYER_CRAWL);	
						playerAnim.enabled = false;
					} else {
						AnimChange (PlayerState.PLAYER_IDLE);	
					}
				}
			}
			//Flip Facing
			if (dir.IsLeft) {
				playerSprite.flipX = true;
			} else if (dir.IsRight) {
				playerSprite.flipX = false;
			}			

			//Ladder
			if (ladderFlag) {
				playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
				if (dir.vertical.y > 0f) {
					AnimChange (PlayerState.PLAYER_CLIMB);
					curPos = playerTransform.localPosition;
					playerTransform.localPosition = curPos + (dir.vertical * speed);
				} else if ((dir.vertical.y < 0f) && (fallFlag)) {
					AnimChange (PlayerState.PLAYER_CLIMB);
					curPos = playerTransform.localPosition;
					playerTransform.localPosition = curPos + (dir.vertical * speed);
				} else if (fallFlag) {
					AnimChange (PlayerState.PLAYER_CLIMB);
					playerAnim.enabled = false;
				} 
			} else {
				playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
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
	void OnLadderEnter()
	{
		ladderFlag = true;
	}
	void OnLadderExit()
	{
		ladderFlag = false;
	}
	void OnUpperCrawlEnter()
	{
		upperCrawlFlag = true;
		CheckCrawl ();
	}
	void OnUpperCrawlExit()
	{
		upperCrawlFlag = false;
		CheckCrawl ();
	}
	void OnLowerCrawlEnter()
	{
		lowerCrawlFlag = true;
		CheckCrawl ();
	}
	void OnLowerCrawlExit()
	{
		lowerCrawlFlag = false;
		CheckCrawl ();
	}
	void CheckCrawl() {
		if ((upperCrawlFlag) && (!lowerCrawlFlag))
			crawlFlag = true;
		else
			crawlFlag = false;
	}

	void OnDestroy()
	{
		joystick.OnJoystickMove -= OnDirChange;
		feetCollider.OnPlatformEnter -= OnFeetPlatformEnter;
		feetCollider.OnPlatformExit -= OnFeetPlatformExit;
		ladderCollider.OnPlatformEnter -= OnLadderEnter;
		ladderCollider.OnPlatformExit -= OnLadderExit;
		upperCrawlCollider.OnPlatformEnter -= OnUpperCrawlEnter;
		upperCrawlCollider.OnPlatformExit -= OnUpperCrawlExit;
		lowerCrawlCollider.OnPlatformEnter -= OnLowerCrawlEnter;
		lowerCrawlCollider.OnPlatformExit -= OnLowerCrawlExit;
	}

	void AnimChange(PlayerState ps, float animSpeed = 1f)
	{
		playerAnim.enabled = true;
		playerAnim.SetInteger ("AnimState",(int)ps);
		playerAnim.speed = animSpeed;
	}

}
