using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	public float deltaX = 50f;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	private GameObject button;
	private BusLevelManager gameMgr;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;


	void Awake() {
		gameMgr = GameObject.Find("BusLevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
	}

	void Start () {
		button = GameObject.Find ("UIManager").GetComponent<HudManager> ().getButton (player);
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
		}catch{
			gameObject.SetActive(false);
		}
		finished = false;
		button.GetComponent<BtnHandler>().OnPressed += move;
	}
	
	private void OnDisable()
	{
		try{
			button.GetComponent<BtnHandler>().OnPressed -= move;
		}catch{
		}
	}

	private void move() {
		if (!finished) {
						Vector3 pos = transform.position;
						pos.x += deltaX;
						transform.position = pos;
				}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Target") {
			finished = true;
			gameMgr.Score(player);
		}
		if (other.name == "FirstLine") {
			other.transform.position = other.transform.position + new Vector3(100f,0,0);
		}
	}
}
