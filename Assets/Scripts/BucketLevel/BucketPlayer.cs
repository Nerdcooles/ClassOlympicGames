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
	
	private LevelManager levelManager;
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
			gameObject.SetActive(false);
		}
	}

	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
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
		shoot_btn.GetComponent<Button>().OnPressed += startPower;
		shoot_btn.GetComponent<Button>().OnReleased += shoot;	
	}
	
	private void OnDisable()
	{
		try{
			shoot_btn.GetComponent<Button>().OnPressed -= startPower;
			shoot_btn.GetComponent<Button>().OnReleased -= shoot;
		}
		catch
		{
		}
	}

	private void startPower() {
		if(levelManager.getState() == LevelManager.eState.Run) {
			can_shoot=true;
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", true);
		}else{
			can_shoot=false;
		}
	}

	private void shoot() {
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
