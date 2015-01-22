using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class SkipperPlayer : LevelPlayer {

	private SkipLevelManager sceneManager;

	Vector2 jump = new Vector2(0,600);
	float speed = 250;
	float turbo = 350;
	float vel;
	
	private bool pressedInJump = false;
	private bool onTheFloor, pressed;

	private float offsetX;

	protected override void Initialize() {
		sceneManager = GameObject.Find("SkipLevelManager").GetComponent<SkipLevelManager>() as SkipLevelManager;
		offsetX = Camera.main.transform.position.x + GameObject.Find("UIManager").GetComponent<UIManager>().SceneWidth;
		finished = false;
		onTheFloor = true;
		vel = speed;
	}

	void Update() {
		if (Camera.main.transform.position.x - transform.position.x > offsetX) {
			Vector3 newPos = new Vector3 (Camera.main.transform.position.x - offsetX, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp (transform.position, newPos, 10 * Time.deltaTime);
			animator.SetBool("isMoving", true);
		}
	}

	void FixedUpdate() {
		if(pressed) {
			animator.SetBool("isMoving", true);
			rigidbody2D.velocity = (Vector2.right * vel);
		}
	}

	IEnumerator RandomWait() {
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
		pressed = true;
		pressedInJump = false;
	}

	protected override void Pressed() {
		if (!finished) {
			if(onTheFloor) {
				pressed = true;
			}else{
				pressedInJump = true;
				rigidbody2D.AddForce (-Vector2.up * 300, ForceMode2D.Impulse);
			}
		}
	}
	
	protected override void Released() {
		if (!finished) {
			if(onTheFloor && pressed) {
				pressed = false;
				onTheFloor = false;
				rigidbody2D.velocity = (Vector2.right * (vel-50));
				rigidbody2D.AddForce (jump, ForceMode2D.Impulse);
				animator.SetBool("isJumping", true);
			}
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Target") {
			finished = true;		

			gameObject.collider2D.enabled = false;

			int pos = sceneManager.Score(player);
			if(pos != GameManager.Instance.getNumPlayer() - 1)
				animator.SetBool("isWinner", true);
			else
				animator.SetBool("isLoser", true);
		}
		if (other.gameObject.tag == "Respawn") {
			pressed = false;
			vel = speed;
			Destroy(other);	
			rigidbody2D.velocity = Vector2.zero;
			animator.SetBool("isMoving", false);
			animator.SetBool("isJumping", false);
		}
		if (other.gameObject.tag == "Bonus") {
			Destroy(other);	
			vel = turbo;
		}
		if (other.gameObject.tag == "Floor") {
			onTheFloor = true;
			if(pressedInJump) {
				pressed = true;
				pressedInJump = false;
			}
			animator.SetBool("isJumping", false);
			animator.SetBool("isMoving", false);
		}
	}
}
