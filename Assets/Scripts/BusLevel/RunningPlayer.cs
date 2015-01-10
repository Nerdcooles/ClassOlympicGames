using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	public float deltaX = 0.5f;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	public GameObject button;
	private BusLevelManager gameMgr;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;


	void Awake() {
		gameMgr = GameObject.Find("BusLevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
	}

	void Start () {
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_bucket");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
		}catch{
			gameObject.SetActive(false);
		}
		finished = false;
	}

	private void OnEnable()
	{
		button.GetComponent<Button>().OnPressed += move;
	}
	
	private void OnDisable()
	{
		try{
		button.GetComponent<Button>().OnPressed -= move;
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
			other.transform.position = other.transform.position + new Vector3(1f,0,0);
		}
	}
}
