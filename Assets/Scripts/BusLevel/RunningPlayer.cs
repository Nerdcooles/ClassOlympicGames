using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : LevelPlayer {

	private BusLevelManager sceneMgr;
	
	private bool finished;

	float destination;
	float step = 50f;
	int speed = 10;

	protected override void Initialize() {
		sceneMgr = GameObject.Find("BusLevelManager").GetComponent<BusLevelManager>() as BusLevelManager;

		destination = transform.position.x;
		finished = false;
	}

	void Update() {
		if (!finished && (destination - transform.position.x) > 1f) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (destination, transform.position.y, transform.position.z), speed * Time.deltaTime);
			animator.SetBool("isMoving", true);
		}else{
			animator.SetBool("isMoving", false);
		}
	}

	protected override void Pressed() {
		if (!finished)
			destination = transform.position.x + step;
		
	}
	
	private void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Target") {
			finished = true;

			Vector3 final_position = transform.position;
			final_position.x = other.transform.position.x + 20f;
			transform.position = final_position;

			int pos = sceneMgr.Score(player);
			if(pos != GameManager.Instance.getNumPlayer() - 1)
				animator.SetBool("isWinner", true);
			else
				animator.SetBool("isLoser", true);
		}
	}
}
