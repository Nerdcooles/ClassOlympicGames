using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class SkatePlayer : LevelPlayer {

	private LongLevelManager sceneMgr;

	public float red_line = -150f;
	Vector2 jump = new Vector2(200.1234f,900);
	float speed = 700;
	private GameObject footPrefab;
	private bool jumped, pressedInJump, pressed;

	protected override void Initialize() {
		Vector3 position = transform.position;
		position.x = -GameObject.Find("UIManager").GetComponent<UIManager>().SceneWidth + 50;
		transform.position = position;
			sceneMgr = GameObject.Find("LongLevelManager").GetComponent<LongLevelManager>() as LongLevelManager;
			finished = false;
			jumped = false;
			pressed = false;
			footPrefab = Resources.Load <GameObject> ("Prefabs/CementFoot");

	}

	void Update() {
		if(!finished) {
			if (transform.position.x >= red_line && !jumped && !finished) {
				Destroy(rigidbody2D);
				animator.SetBool("isLoser", true);
				sceneMgr.Score(player, 0f);
				finished = true;	
			}
		}
	}
	
	
	void FixedUpdate() {
		if(!finished) {
			if(pressed && !jumped) {
				rigidbody2D.velocity = (Vector2.right * speed);
			}
		}
	}

	protected override void Pressed() {
		if (!finished) {
			if(!jumped) {
				pressed = true;
			}else{
				pressedInJump = true;
				rigidbody2D.AddForce (-Vector2.up * 500, ForceMode2D.Impulse);
			}
		}
	}
	
	protected override void Released() {
		if (!finished) {
			if(!jumped && pressed) {
				pressed = false;
				jumped = true;
				rigidbody2D.gravityScale = 100f;
				rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
				animator.SetBool("isJumping", true);
			}
		}

	}
	
	private void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "Target"){
			if (other.gameObject.name == "cement"+ (player.GetHashCode()+1)) {
				finished = true;
				rigidbody2D.gravityScale = 0;
				Instantiate(footPrefab, transform.position - new Vector3(0f,50f,-1f), transform.rotation);
					Debug.Log(player.ToString() + " " + (transform.position.x - red_line));
				sceneMgr.Score(player, transform.position.x - red_line);
				rigidbody2D.velocity = Vector2.zero;
				
				animator.SetBool("isJumping", false);
				animator.SetBool("isMoving", false);
			}
		}else{

			if(other.tag == "Bound"){
					finished = true;
					rigidbody2D.gravityScale = 0;
					Debug.Log(player.ToString() + " " + (transform.position.x - red_line));
					sceneMgr.Score(player, 0);
					rigidbody2D.velocity = Vector2.zero;
					
					animator.SetBool("isJumping", false);
				animator.SetBool("isMoving", false);
				animator.SetBool("isLoser", true);
			}
		}
	}
}
