using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	private GameManager.eColors color;
	
	public GameObject shoot_btn;
	public GameObject ballPrefab;
	public int alpha = 500;
	
	private BucketLevelManager levelManager;
	private float force;
	private float press_time;
	private Vector3 direction;
	private Animator animator;
	private bool can_shoot;
	private RuntimeAnimatorController animCtrl;

	void Awake() {
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
		}catch{
			Debug.LogError(player.ToString() + " no color");
			gameObject.SetActive(false);
		}
	}

	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
		can_shoot = false;
		switch(player) {
		case GameManager.ePlayers.p01:  
		case GameManager.ePlayers.p02:direction = (Quaternion.AngleAxis(60, transform.forward) * transform.right) * alpha; break;
		case GameManager.ePlayers.p03:
		case GameManager.ePlayers.p04:direction = (Quaternion.AngleAxis(60, transform.forward) * transform.right) * alpha; break;
		}
	}
	
	private void OnEnable()
	{
		shoot_btn.GetComponent<PressGesture>().Pressed += startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released += shoot;	
	}
	
	private void OnDisable()
	{
		try{
		shoot_btn.GetComponent<PressGesture>().Pressed -= startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released -= shoot;
		}
		catch
		{
		}
	}

	private void startPower(object sender, EventArgs e) {
		if(levelManager.isStarted() && !levelManager.isGameover()) {
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
			force = (float)Math.Round((Time.time - press_time), 3);
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
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
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<BucketBall>().getPlayer() != player) {
			can_shoot=false;
			Destroy(other.gameObject);
			animator.SetBool("isHitted",true);
			
		}
	}
}
