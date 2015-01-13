using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	private const int MAX = 500;

	private int speed = 0;
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	private GameObject button;
	private BusLevelManager sceneManager;
	private Animator animator;
	private bool finished;
	private int last;
	private RuntimeAnimatorController animCtrl;


	void Awake() {
		sceneManager = GameObject.Find("BusLevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
	}

	void Start () {
		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		try {
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_runner");
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
			pos.x += 40f;
			rigidbody2D.MovePosition(pos);
			speed = 20;
			CancelInvoke("DecrementSpeed");
			InvokeRepeating ("DecrementSpeed", 0.1f, 0.2f);
		}
	}
	
	private void DecrementSpeed() {
		speed -= 10;
		if (speed <= 0) {
			speed = 0;
			CancelInvoke ("DecrementSpeed");
	    }
		animator.SetInteger("speed", speed);
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Target") {
			finished = true;
			int pos = sceneManager.Score(player);
			if(pos != GameManager.Instance.getNumPlayer() - 1)
				animator.SetBool("isWinner", true);
			else
				animator.SetBool("isLoser", true);
			Debug.Log(pos);
		}
		if (other.name == "FirstLine") {
			other.transform.position = other.transform.position + new Vector3(100f,0,0);
		}
	}
}
