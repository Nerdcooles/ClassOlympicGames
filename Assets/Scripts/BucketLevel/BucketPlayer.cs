using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	public GameObject shoot_btn;
	public GameObject ballPrefab;
	public int alpha = 500;
	
	private BucketLevelManager levelManager;
	private float force;
	private float press_time;
	private Vector3 direction;
	private Animator animator;
	private bool can_shoot;

	public Sprite btn_pressed;
	public Sprite btn_released;

	
	
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
		shoot_btn.GetComponent<SpriteRenderer>().sprite = btn_released;
		animator = GetComponent<Animator>();
		can_shoot = false;
		switch(player) {
		case GameManager.ePlayers.p01:  
		case GameManager.ePlayers.p02:direction = (Quaternion.AngleAxis(50, transform.forward) * transform.right) * alpha; break;
		case GameManager.ePlayers.p03:
		case GameManager.ePlayers.p04:direction = (Quaternion.AngleAxis(40, transform.forward) * transform.right) * alpha; break;
		}
	}
	
	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		shoot_btn.GetComponent<PressGesture>().Pressed += startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released += shoot;
		
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
		shoot_btn.GetComponent<PressGesture>().Pressed -= startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released -= shoot;
		}
		catch
		{
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void startPower(object sender, EventArgs e) {
		if(levelManager.isStarted() && !levelManager.isGameover()) {
			shoot_btn.GetComponent<SpriteRenderer>().sprite = btn_pressed;
			can_shoot=true;
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", true);
		}else{
			can_shoot=false;
		}
	}

	private void shoot(object sender, EventArgs e) {
		if(can_shoot) {
			shoot_btn.GetComponent<SpriteRenderer>().sprite = btn_released;
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
			force = Time.time - press_time;
		}
	}
	
	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.2f);
		GameObject ballInstance = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
		ballInstance.GetComponent<BucketBall>().setPlayer(player);
		ballInstance.rigidbody2D.AddForce(direction * force);
	}

	public void endShooting() {
		animator.SetBool("isShooting",false);
	}
}
