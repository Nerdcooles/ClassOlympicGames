using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	public float deltaX = 1;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	public GameObject button;
	private LevelManager levelManager;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;


	void Awake() {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
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
		last = GameManager.Instance.getNumPlayer();
	}

	private void OnEnable()
	{
		button.GetComponent<Button>().OnPressed += move;
	}
	
	private void OnDisable()
	{
		button.GetComponent<Button>().OnPressed -= move;
	}

	private void move() {
		Debug.Log(player.ToString() + " tap");

			Vector3 pos = transform.position;
			pos.x += deltaX;
			transform.position = pos;

	}


//	private void OnTriggerEnter2D(Collider2D other) {
//		int pos = levelManager.Finish(this.player)+1;
//		if(pos!=last || last==1) animator.SetBool("isWinner", true);
//		else animator.SetBool("isLoser", true);
//		finished = true;
//	}
}
