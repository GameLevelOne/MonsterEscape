using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
	PLAYER_DAMAGE,
	PLAYER_PLANKFALL,
	PLAYER_CLIMBPLATFORM
}


public class Player : MonoBehaviour {

	Transform playerTransform;
	Animator playerAnim;
	SpriteRenderer playerSprite;
	Rigidbody2D playerRigidBody;

	public TriggerCollider feetCollider;
	public TriggerCollider ladderCollider;
	public TriggerCollider upperCrawlCollider;
	public TriggerCollider lowerCrawlCollider;
	public TriggerCollider lowerPlankCollider;
	public TriggerCollider lowerClimbAbleCollider;

	public Joystick joystick;
	public ActionButton actionButton;

	public float speed = 10f;
	float prevSpeed = 10f;
	JoystickDirection dir;
	bool fallFlag = true;
	int fallStack;
	bool ladderFlag = false;
	bool onPlankFlag = false;
	bool plankFallFlag = false;
	public float plankThreshold;
	bool crawlFlag = false;
	bool upperCrawlFlag = false;
	bool lowerCrawlFlag = false;
	bool climbAbleFlag = false;
	bool climbingFlag = false;
	ClimbAble climbTrigger;

	PlayerState playerAction = PlayerState.PLAYER_IDLE;

	void Start () {
		playerTransform = GetComponent<Transform> ();
		playerAnim = GetComponent<Animator> ();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerRigidBody = GetComponent<Rigidbody2D> ();

		dir = JoystickDirection.zero;
		fallStack = 0;
		joystick.OnJoystickMove += OnDirChange;
		feetCollider.OnTriggerEnter += OnFeetPlatformEnter;
		feetCollider.OnTriggerExit += OnFeetPlatformExit;
		ladderCollider.OnTriggerEnter += OnLadderEnter;
		ladderCollider.OnTriggerExit += OnLadderExit;
		upperCrawlCollider.OnTriggerEnter += OnUpperCrawlEnter;
		upperCrawlCollider.OnTriggerExit += OnUpperCrawlExit;
		lowerCrawlCollider.OnTriggerEnter += OnLowerCrawlEnter;
		lowerCrawlCollider.OnTriggerExit += OnLowerCrawlExit;
		actionButton.OnActionDown += OnActionDown;
		actionButton.OnActionUp += OnActionUp;
		lowerPlankCollider.OnTriggerEnter += OnLowerPlankEnter;
		lowerPlankCollider.OnTriggerExit += OnLowerPlankExit;
		lowerClimbAbleCollider.OnTriggerEnter += OnClimbAbleEnter;
		lowerClimbAbleCollider.OnTriggerExit += OnClimbAbleExit;
	}

	public JoystickDirection playerDir {
		get {
			return dir;
		}
	}

	void Update () 
	{
		if (speed > 0) {
			Vector2 curPos = playerTransform.localPosition;

			//Eat
			if (playerAction >= PlayerState.PLAYER_EAT) {
				AnimChange (playerAction);
			} else if (plankFallFlag) {
				AnimChange (PlayerState.PLAYER_PLANKFALL);
			} else if (climbingFlag) {
				AnimChange (PlayerState.PLAYER_CLIMBPLATFORM);
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
							if ((onPlankFlag) && (Mathf.Abs (dir.horizontal.x) >= plankThreshold)) {
								plankFallFlag = true;
							} else {
								AnimChange (PlayerState.PLAYER_WALK, 0.5f + (Mathf.Abs (dir.horizontal.x) / 2));	
							}
						}						
					}
					if ((!fallFlag) || (!ladderFlag)) {
						playerTransform.localPosition = curPos + (dir.horizontal * speed * Time.deltaTime);
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
						playerTransform.localPosition = curPos + (dir.vertical * speed * Time.deltaTime);
					} else if ((dir.vertical.y < 0f) && (fallFlag)) {
						AnimChange (PlayerState.PLAYER_CLIMB);
						curPos = playerTransform.localPosition;
						playerTransform.localPosition = curPos + (dir.vertical * speed * Time.deltaTime);
					} else if (fallFlag) {
						AnimChange (PlayerState.PLAYER_CLIMB);
						playerAnim.enabled = false;
					} 
				} else if (climbAbleFlag){
					playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
					if (dir.vertical.y > 0f) {
						climbingFlag = true;
					} 
				} else {
					playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			}
		}
	}

	public void PlayerAction(PlayerState state)
	{
		playerAction = state;
	}
		
	void FinishClimb() {
		climbingFlag = false;
		climbTrigger.SetPlayerLocation (transform);
		AnimChange (PlayerState.PLAYER_IDLE);
	}

	void OnDirChange(JoystickDirection newDir)
	{
		dir = newDir;	
	}

	void OnFeetPlatformEnter(GameObject other)
	{
		fallFlag = false;
		fallStack++;
	}
	void OnFeetPlatformExit(GameObject other)
	{
		fallStack--;
		if (fallStack<=0)
			fallFlag = true;
	}
	void OnLadderEnter(GameObject other)
	{
		ladderFlag = true;
	}
	void OnLadderExit(GameObject other)
	{
		ladderFlag = false;
	}
	void OnUpperCrawlEnter(GameObject other)
	{
		upperCrawlFlag = true;
		CheckCrawl ();
	}
	void OnUpperCrawlExit(GameObject other)
	{
		upperCrawlFlag = false;
		CheckCrawl ();
	}
	void OnLowerCrawlEnter(GameObject other)
	{
		lowerCrawlFlag = true;
		CheckCrawl ();
	}
	void OnLowerCrawlExit(GameObject other)
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
	void OnLowerPlankEnter(GameObject other)
	{
		onPlankFlag = true;
		fallFlag = false;
		fallStack++;
	}
	void OnLowerPlankExit(GameObject other)
	{
		onPlankFlag = false;
		fallStack--;
		if (fallStack<=0)
			fallFlag = true;
	}
	void OnClimbAbleEnter(GameObject other)
	{
		climbAbleFlag = true;
		climbTrigger = other.GetComponent<ClimbAble> ();
	}
	void OnClimbAbleExit(GameObject other)
	{
		climbAbleFlag = false;
		climbTrigger = null;
	}

	void OnDestroy()
	{
		joystick.OnJoystickMove -= OnDirChange;
		feetCollider.OnTriggerEnter -= OnFeetPlatformEnter;
		feetCollider.OnTriggerExit -= OnFeetPlatformExit;
		ladderCollider.OnTriggerEnter -= OnLadderEnter;
		ladderCollider.OnTriggerExit -= OnLadderExit;
		upperCrawlCollider.OnTriggerEnter -= OnUpperCrawlEnter;
		upperCrawlCollider.OnTriggerExit -= OnUpperCrawlExit;
		lowerCrawlCollider.OnTriggerEnter -= OnLowerCrawlEnter;
		lowerCrawlCollider.OnTriggerExit -= OnLowerCrawlExit;
		lowerPlankCollider.OnTriggerEnter -= OnLowerPlankEnter;
		lowerPlankCollider.OnTriggerExit -= OnLowerPlankExit;
		actionButton.OnActionDown -= OnActionDown;
		actionButton.OnActionUp -= OnActionUp;
		lowerClimbAbleCollider.OnTriggerEnter -= OnClimbAbleEnter;
		lowerClimbAbleCollider.OnTriggerExit -= OnClimbAbleExit;
	}

	void AnimChange(PlayerState ps, float animSpeed = 1f)
	{
		playerAnim.enabled = true;
		playerAnim.SetInteger ("AnimState",(int)ps);
		playerAnim.speed = animSpeed;
	}

	void StandUp() {
		plankFallFlag = false;
	}

	public void SetPause(bool paused)
	{
		if (paused) {
			prevSpeed = speed;
			speed = 0;
			playerAnim.enabled = false;
		} else {
			speed = prevSpeed;
			playerAnim.enabled = true;
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "HideAble") {
			actionButton.Activate(PlayerState.PLAYER_HIDE,"HIDE");
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "HideAble") {
			actionButton.Deactivate();
		}
	}


	void OnActionDown(PlayerState state) {
		PlayerAction (state);
	}
	void OnActionUp(PlayerState state) {
		PlayerAction (PlayerState.PLAYER_IDLE);
	}

}
