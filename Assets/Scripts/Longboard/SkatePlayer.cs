using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class SkatePlayer : MonoBehaviour {

	public float red_line = -150f;
	public Vector2 jump;
	public float normal_vel;
	public float slow_vel;
	public float fast_vel;
	private int speed = 0;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	private GameObject button;
	private LongLevelManager sceneManager;
	private LevelManager lvm;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;
	private float press_time;
	private GameObject footPrefab;
	private bool jumping, can_move, pressed;
	private float offsetX;

	void Start () {
		sceneManager = GameObject.Find("LongLevelManager").GetComponent<LongLevelManager>() as LongLevelManager;
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		offsetX = Camera.main.transform.position.x - transform.position.x;
		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		if (GameManager.Instance.IsPlaying (player)) {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_runner");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
			finished = false;
			button.GetComponent<BtnHandler>().OnPressed += Move;
			button.GetComponent<BtnHandler>().OnReleased += Jump;
			jumping = false;
			can_move = true;
			pressed = false;
			footPrefab = Resources.Load <GameObject> ("Prefabs/CementFoot");
			lvm.OnFinish += endPlayer;
		}else{
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if (transform.position.x >= red_line && !jumping && !finished) {
			can_move = false;
			rigidbody2D.velocity = Vector2.zero;
			animator.SetBool("isLoser", true);
			sceneManager.Score(player, 0f);
			finished = true;	
		}
	}

	private void Move() {
		pressed = true;
		if (!finished && can_move) {
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.right * fast_vel);
		}
	}
	
	private void Jump() {
		if (!finished && can_move && pressed) {
						jumping = true;
			can_move = false;
			rigidbody2D.gravityScale = 40;
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
			animator.SetBool("isJumping", true);

		}

	}
	
	public void endPlayer() {
		//IF NOT LAST PLAYER
		try {
						int num_players = GameManager.Instance.getNumPlayer ();
						if (num_players == 1 || lvm.getPodium (num_players - 1) != this.player) {
								//IF SINGLE PLAYER OR NOT LAST PLAYER
								animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_winner");
						} else {
								animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
						}
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
			
						GameObject medal = Resources.Load<GameObject> ("Prefabs/Medal_" + lvm.GetPosition (player));
						Instantiate (medal, transform.position + new Vector3 (0f, 90f, 0f), transform.rotation);
				} catch (Exception ex) {
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
		animator = GetComponent<Animator> ();			
		animator.runtimeAnimatorController = animCtrl;
				}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Bonus") {
			Destroy(other);	
			rigidbody2D.velocity = (Vector2.right * fast_vel);
		}
		if (other.gameObject.tag == "Floor") {
			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);

		}
			if (other.name == "cement"+ (player.GetHashCode()+1)) {
			finished = true;	
			Instantiate(footPrefab, transform.position - new Vector3(0f,50f,-1f), transform.rotation);
			sceneManager.Score(player, transform.position.x - red_line);
			rigidbody2D.velocity = Vector2.zero;

			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);
			
		}
	}
}
