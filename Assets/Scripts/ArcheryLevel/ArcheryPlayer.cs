using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections;

public class ArcheryPlayer : MonoBehaviour {
	
	public GameManager.ePlayers player;
	private GameManager.eColors color;

	private float pencilPosX = -32.6f;
	private float pencilPosY = -9f;
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
		if(transform.rotation.y != 0)
			pencilPosX = -pencilPosX;

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
			pencilInstance = Instantiate(pencilPrefab, transform.position + new Vector3(pencilPosX, pencilPosY, 0f) , Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f))) as GameObject;
			pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", false);
			InvokeRepeating ("SpinPencil", 0.1f, 0.01f);
		}
	}
	
	void SpinPencil() {
		try{
		pencilInstance.transform.Rotate (Vector3.forward, Time.deltaTime * 180, Space.Self);
		}catch{
				}
	}
	
	private void shoot() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			CancelInvoke("SpinPencil");
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
		}
	}

	

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		try{
		pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			pencilInstance.rigidbody2D.AddForce(pencilInstance.transform.right * 500, ForceMode2D.Impulse);
		}catch{
		}
		yield return new WaitForSeconds(1f);
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
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<ArcheryPencil>().getPlayer() != player) {
			can_shoot=false;
			Destroy(other.gameObject);
			Destroy(pencilInstance);
			animator.SetBool("isHitted",true);

		}
	}
}
