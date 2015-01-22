using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class SkatePlayer : LevelPlayer {

	private LongLevelManager sceneMgr;

	public float red_line = -150f;
	public Vector2 jump;
	public float normal_vel;
	public float slow_vel;
	public float fast_vel;
	private int speed = 0;
	private bool finished;
	private int last;
	private float press_time;
	private GameObject footPrefab;
	private bool jumping, can_move, pressed;

	protected override void Initialize() {
		Vector3 position = transform.position;
		position.x = -GameObject.Find("UIManager").GetComponent<UIManager>().SceneWidth + 50;
		transform.position = position;
			sceneMgr = GameObject.Find("LongLevelManager").GetComponent<LongLevelManager>() as LongLevelManager;
			finished = false;
			jumping = false;
			can_move = true;
			pressed = false;
			footPrefab = Resources.Load <GameObject> ("Prefabs/CementFoot");

	}

	void Update() {
		if (transform.position.x >= red_line && !jumping && !finished) {
			can_move = false;
			rigidbody2D.velocity = Vector2.zero;
			animator.SetBool("isLoser", true);
			sceneMgr.Score(player, 0f);
			finished = true;	
		}
	}

	protected override void Pressed() {
		pressed = true;
		if (!finished && can_move) {
			rigidbody2D.gravityScale = 0;
			rigidbody2D.velocity = (Vector2.right * fast_vel);
		}
	}
	
	protected override void Released() {
		if (!finished && can_move && pressed) {
						jumping = true;
			can_move = false;
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
			if (other.name == "cement"+ (player.GetHashCode()+1)) {
			finished = true;	
			Instantiate(footPrefab, transform.position - new Vector3(0f,50f,-1f), transform.rotation);
			sceneMgr.Score(player, transform.position.x - red_line);
			rigidbody2D.velocity = Vector2.zero;

			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);
			
		}
	}
}
