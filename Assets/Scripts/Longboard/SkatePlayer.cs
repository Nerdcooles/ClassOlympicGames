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

	private bool jumping, can_move, pressed;
	private float offsetX;

	void Awake() {
	}

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
		pressed = false;
		if (!finished && can_move) {
						jumping = true;
						rigidbody2D.gravityScale = 40;
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
			animator.SetBool("isJumping", true);

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
			if (other.name == "cement") {
			finished = true;	
			sceneManager.Score(player, transform.position.x - red_line);
			can_move = false;
			rigidbody2D.velocity = Vector2.zero;

			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);
			
		}
	}
}
