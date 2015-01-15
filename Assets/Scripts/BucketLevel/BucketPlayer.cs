using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	private GameManager.eColors color;

	private const int GRAVITY = 100;
	private GameObject button;
	private GameObject ballPrefab;
	public int alpha = 500; //TODO remove alpha

	private BucketLevelManager sceneMgr;
	private LevelManager lvm;
	private float force;
	private float angle;
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
		if (GameManager.Instance.IsPlaying (player)) {
						can_shoot = true;
						if(transform.position.y < 0)
							angle = 65f;
						else 
							angle = 60f;
						direction = (Quaternion.AngleAxis (angle, transform.forward) * transform.right) * alpha;

						button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
						color = GameManager.Instance.getColor (player);
						animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString () + "/animation/" + color.ToString () + "_bucket");
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
						ballPrefab = Resources.Load <GameObject> ("Prefabs/BucketBall");
						button.GetComponent<BtnHandler> ().OnPressed += startPower;
						button.GetComponent<BtnHandler> ().OnReleased += shoot;
						lvm.OnFinish += endPlayer;
				} else {
						gameObject.SetActive (false);
				}
	}
	
	private void OnDisable()
	{
		try{
			button.GetComponent<BtnHandler>().OnPressed -= startPower;
			button.GetComponent<BtnHandler>().OnReleased -= shoot;
			lvm.OnFinish -= endPlayer;
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
		animator.SetBool("isLoading",false);
		press_time = Time.time;	
		can_shoot=true;
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
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<BucketBall>().getPlayer() != player) {
			can_shoot=false;
			Destroy(other.gameObject);
			animator.SetBool("isHitted",true);
		}
	}
}
