﻿using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : LevelPlayer {

	private BucketLevelManager sceneMgr;

	private const int GRAVITY = 100;
	private GameObject ballPrefab;
	public int alpha = 500; //TODO remove alpha

	private float force;
	private float angle;
	private float press_time;
	private Vector3 direction;
	private bool can_shoot = true;
	private bool pressed = false;

	protected override void Initialize(){
		sceneMgr = GameObject.Find("BucketLevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;

						if(transform.position.y < 0)
							angle = 65f;
						else 
							angle = 60f;
						direction = (Quaternion.AngleAxis (angle, transform.forward) * transform.right) * alpha;


						ballPrefab = Resources.Load <GameObject> ("Prefabs/BucketBall");

	}
		
	protected override void Pressed() {
		if(lvm.State == LevelManager.eState.Run && can_shoot) {
			pressed = true;
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
		}
	}

	protected override void Released() {
		if(lvm.State == LevelManager.eState.Run && can_shoot && pressed) {
			force = (float)Math.Round((Time.time - press_time), 1) * GRAVITY;
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
			pressed = false;
		}
	}
	
	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		GameObject ballInstance = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
		ballInstance.GetComponent<BucketBall>().setPlayer(player);
		ballInstance.rigidbody2D.AddForce(direction * force);
	}

	public void endShooting() {
		animator.SetBool("isShooting",false);
	}

	public void endHitted() {
		animator.SetBool("isHitted",false);
		animator.SetBool("isLoading",false);
		animator.SetBool("isShooting",false);
		press_time = Time.time;	
		can_shoot=true;
	}
	
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Bullet"&& other.gameObject.GetComponent<BucketBall>().getPlayer() != player) {
			Destroy(other);
			animator.SetBool("isHitted",true);
			can_shoot = false;
			pressed = false;
		}
	}
}
