using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections;

public class ArcheryPlayer : MonoBehaviour {
	
	public GameManager.ePlayers player;
	private GameManager.eColors color;

	private Vector3 pencilPos = new Vector3 (-32.6f, -9f, 0f);
	private GameObject button;
	private GameObject pencilPrefab;
	
	private float force;
	private float press_time;
	private Vector3 direction;
	private GameObject pencilInstance;
	private bool can_shoot;

	private ArcheryLevelManager sceneMgr;
	private LevelManager lvm;

	private Animator animator;
	private RuntimeAnimatorController animCtrl;

	void Awake() {
		sceneMgr = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}
	
	void Start () {
		can_shoot = true;

		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
			pencilPrefab = Resources.Load <GameObject> ("Prefabs/ArcheryPencil");
		}catch{
			gameObject.SetActive(false);
		}
		button.GetComponent<BtnHandler>().OnPressed += startPower;
		button.GetComponent<BtnHandler>().OnReleased += shoot;
		lvm.OnFinish += endPlayer;
	}

	private void startPower() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			pencilInstance = Instantiate(pencilPrefab, transform.position + pencilPos, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f))) as GameObject;
			pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", false);
			InvokeRepeating ("SpinPencil", 0.1f, 0.01f);
			Debug.Log("Invoked");
		}
	}
	
	void SpinPencil() {
		pencilInstance.transform.Rotate (Vector3.forward, Time.deltaTime * 180, Space.Self);
	}
	
	private void shoot() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			//TODO set spin
			Debug.Log("Shoot");
			CancelInvoke("SpinPencil");
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
		}
	}

	

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
		pencilInstance.rigidbody2D.AddForce(pencilInstance.transform.right * 500, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
	}
	
	public void endShooting() {
		animator.SetBool("isShooting",false);
	}
	
	public void endHitted() {
		animator.SetBool("isHitted",false);
		can_shoot=true;
	}
	
//	void OnTriggerEnter2D(Collider2D other) {
//		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<BucketBall>().getPlayer() != player) {
//			can_shoot=false;
//			Destroy(other.gameObject);
//			animator.SetBool("isHitted",true);
//			
//		}
//	}
	
//	private void OnEnable()
//	{
//		shoot_btn.GetComponent<BtnHandler>().OnPressed += shoot;
//	}
//	
//	private void OnDisable()
//	{
//		shoot_btn.GetComponent<BtnHandler>().OnPressed -= shoot;
//	}
//
//	
//	private void shoot() {
//		if(!shooted && pencilInstance!=null) {
//			pencilInstance.GetComponent<ArcheryPencil>().shoot();
//			StartCoroutine(Reload());
//			shooted = true;
//		}
//	}
//
//	IEnumerator Reload() {
//		yield return new WaitForSeconds(1f);
//		can_reload = true;
	//	}
	
	public void endPlayer() {
		//IF NOT LAST PLAYER
		int num_players = GameManager.Instance.getNumPlayer ();
		if (num_players == 1 || lvm.getPodium (num_players - 1) != this.player) {
			//IF SINGLE PLAYER OR NOT LAST PLAYER
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_winner");
			Debug.Log("WIN " + "Sprites/Podium/" + color.ToString () + "_podium_winner");
		} else {
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
			Debug.Log("LOSE " + "Sprites/Podium/" + color.ToString () + "_podium_loser");
		}
		animator = GetComponent<Animator>();			
		animator.runtimeAnimatorController = animCtrl;
	}
}

//	public GameManager.ePlayers player;
//	public GameObject pencilPrefab;
//	public float force = 300f;
//	private GameObject pencil;
//	private bool canShoot = false;
//
//	void Start() {
//		newPencil();
//	}	
//
//	public void setDirection(float dir) {
//		if(canShoot)
//		{		
//			if(player == GameManager.ePlayers.p01 || player == GameManager.ePlayers.p04)
//				pencil.GetComponent<ArcheryPencil>().setDirection(dir*60);
//			  else 
//			   pencil.GetComponent<ArcheryPencil>().setDirection(180-dir*60);
//		}
//	}
//
//	public void shoot() {
//		if(canShoot) {
//			pencil.rigidbody2D.AddForce(pencil.transform.right * force);
//			canShoot = false;
//		}
//	}
//
//	public void newPencil() {
//		pencil = Instantiate(pencilPrefab, transform.position, transform.rotation) as GameObject;
//		pencil.GetComponent<ArcheryPencil>().setPlayer(player);
//		canShoot = true;
//	}
