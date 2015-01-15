using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class SkipperPlayer : MonoBehaviour {

	private const int MAX = 500;
	public Vector2 jump;
	public float normal_vel;
	public float slow_vel;
	public float fast_vel;
	private int speed = 0;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	private GameObject button;
	private SkipLevelManager sceneManager;
	private LevelManager lvm;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;
	private float press_time;

	private bool can_jump, pressed;

	void Awake() {
		sceneManager = GameObject.Find("SkipLevelManager").GetComponent<SkipLevelManager>() as SkipLevelManager;
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
	}

	void Start () {
		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		if (GameManager.Instance.IsPlaying (player)) {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_runner");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
			finished = false;
			button.GetComponent<BtnHandler>().OnPressed += Move;
			button.GetComponent<BtnHandler>().OnReleased += Jump;
			can_jump = true;
		}else{
			gameObject.SetActive(false);
		}
	}

	private void Move() {
		if (!finished && can_jump) {
			pressed = true;
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.right * normal_vel);
		}
	}
	
	private void Jump() {
		if (!finished && can_jump && pressed) {
			pressed = false;
						can_jump = false;
						rigidbody2D.gravityScale = 40;
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
				}

	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Target") {
			finished = true;			
			int pos = sceneManager.Score(player);
			if(pos != GameManager.Instance.getNumPlayer() - 1)
				animator.SetBool("isWinner", true);
			else
				animator.SetBool("isLoser", true);
		}
		if (other.gameObject.tag == "Respawn") {
			pressed = false;

			Destroy(other);	
			rigidbody2D.gravityScale = 40;
			rigidbody2D.velocity = (Vector2.right * slow_vel);
		}
		if (other.gameObject.tag == "Floor") {
			can_jump = true;
		}

		if (other.gameObject.tag == "RearWall") {
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.right * normal_vel);
		}	
	}
}
