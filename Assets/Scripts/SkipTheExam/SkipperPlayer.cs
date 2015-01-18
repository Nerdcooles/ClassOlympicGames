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
	private float offsetX;

	void Awake() {
	}

	void Start () {
		sceneManager = GameObject.Find("SkipLevelManager").GetComponent<SkipLevelManager>() as SkipLevelManager;
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
			can_jump = true;
			lvm.OnFinish += endPlayer;
		}else{
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if (Camera.main.transform.position.x - transform.position.x > offsetX) {
			Vector3 newPos = new Vector3 (Camera.main.transform.position.x - offsetX, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, newPos, 10 * Time.deltaTime);
			animator.SetBool("isMoving", true);
		}

	}

	private void Move() {
		if (!finished && can_jump) {
			pressed = true;
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.right * normal_vel);
			animator.SetBool("isMoving", true);
		}
	}
	
	private void Jump() {
		if (!finished && can_jump && pressed) {
			pressed = false;
						can_jump = false;
						rigidbody2D.gravityScale = 40;
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
			animator.SetBool("isJumping", true);

		}

	}
	
	public void endPlayer() {
		//IF NOT LAST PLAYER
		int num_players = GameManager.Instance.getNumPlayer ();
		if (num_players == 1 || lvm.getPodium (num_players - 1) != this.player) {
			//IF SINGLE PLAYER OR NOT LAST PLAYER
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_winner");
		} else {
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
		}
		animator = GetComponent<Animator>();			
		animator.runtimeAnimatorController = animCtrl;
		
		GameObject medal = Resources.Load<GameObject>("Prefabs/Medal_" + lvm.GetPosition(player));
		Instantiate(medal, transform.position + new Vector3(0f,90f,0f), transform.rotation);
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
			animator.SetBool("isMoving", false);
			animator.SetBool("isJumping", false);
		}
		if (other.gameObject.tag == "Bonus") {
			Destroy(other);	
			rigidbody2D.velocity = (Vector2.right * fast_vel);
		}
		if (other.gameObject.tag == "Floor") {
			can_jump = true;
			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);

		}
	}
}
