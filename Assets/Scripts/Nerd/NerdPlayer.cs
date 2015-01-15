using UnityEngine;
using System.Collections;

public class NerdPlayer : MonoBehaviour {
	
	public GameManager.ePlayers player;
	private GameManager.eColors color;

	private GameObject button;
	private GameObject nerdPrefab;
	
	private float force;
	private Vector3 direction;
	private GameObject nerdInstance;

	private NerdLevelManager sceneMgr;
	private LevelManager lvm;
	
	private Animator animator;
	private RuntimeAnimatorController animCtrl;
	
	void Awake() {
		sceneMgr = GameObject.Find("NerdLevelManager").GetComponent<NerdLevelManager>() as NerdLevelManager;
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}
	
	void Start () {
		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
			nerdPrefab = Resources.Load <GameObject> ("Prefabs/Nerd");

		}catch{
			gameObject.SetActive(false);
		}
		button.GetComponent<BtnHandler>().OnPressed += shoot;
		lvm.OnFinish += endPlayer;
		lvm.OnStart += StartPlayer;
	}

	void StartPlayer() {
		nerdInstance = Instantiate(nerdPrefab, transform.position - new Vector3(20f, 20f, 0), Quaternion.Euler(new Vector3(0, 0, -45f))) as GameObject;
		animator.SetBool("isLoading", true);

		}

	//pencilInstance.transform.Rotate (Vector3.forward, Time.deltaTime * 180, Space.Self);

	
	private void shoot() {
		if(lvm.getState() == LevelManager.eState.Run) {
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
		}
	}

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		try{
			nerdInstance.GetComponent<Nerd>().Shooted = true;
		}catch{
		}
		yield return new WaitForSeconds(1f);
	}
	
	public void endShooting() {
		animator.SetBool("isShooting",false);
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
}
