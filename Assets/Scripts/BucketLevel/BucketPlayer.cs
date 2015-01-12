using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	private GameManager.eColors color;

	private const int GRAVITY = 100;
	private GameObject button;
	public GameObject ballPrefab;
	public int alpha = 500;
	
	private BucketLevelManager sceneMgr;
	private LevelManager lvm;
	private float force;
	private float press_time;
	private Vector3 direction;
	private Animator animator;
	private bool can_shoot;
	private RuntimeAnimatorController animCtrl;

	void Awake() {
		sceneMgr = GameObject.Find("BucketLevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	void Start () {
		can_shoot = true;
		switch(player) {
		case GameManager.ePlayers.p01:  
		case GameManager.ePlayers.p02:direction = (Quaternion.AngleAxis(65, transform.forward) * transform.right) * alpha; break;
		case GameManager.ePlayers.p03:
		case GameManager.ePlayers.p04:direction = (Quaternion.AngleAxis(60, transform.forward) * transform.right) * alpha; break;
		}
		button = GameObject.Find ("UIManager").GetComponent<HudManager> ().getButton (player);
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
		}catch{
			gameObject.SetActive(false);
		}
		button.GetComponent<BtnHandler>().OnPressed += startPower;
		button.GetComponent<BtnHandler>().OnReleased += shoot;
		//lvm.OnFinish += endPlayer;
	}
	
	private void OnDisable()
	{
		try{
			button.GetComponent<BtnHandler>().OnPressed -= startPower;
			button.GetComponent<BtnHandler>().OnReleased -= shoot;
			//lvm.OnFinish -= endPlayer;
		}catch{
		}
	}

	private void startPower() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", false);
		}
	}

	private void shoot() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			force = (float)Math.Round((Time.time - press_time), 1) * GRAVITY;
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
		can_shoot=true;
	}

//	public void endPlayer() {
//		animator.SetBool("isHitted",false);
//		animator.SetBool("isLoading", false);
//		animator.SetBool("isShooting", false);
//		//IF NOT LAST PLAYER
//		if(lvm.getPodium(GameManager.Instance.getNumPlayer()-1)!=this.player)
//			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString() + "_podium_winner");
//		else
//			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString() + "_podium_loser");
//		animator.runtimeAnimatorController = animCtrl;
//	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<BucketBall>().getPlayer() != player) {
			can_shoot=false;
			Destroy(other.gameObject);
			animator.SetBool("isHitted",true);
			
		}
	}
}
