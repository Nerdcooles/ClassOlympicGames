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

	bool stop = true;
	float x, old_pos;


	void Start () {
		sceneManager = GameObject.Find("BusLevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
		if (GameManager.Instance.IsPlaying (player)) {
			button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
			button.GetComponent<BtnHandler>().OnPressed += move;
			finished = false;
			x = transform.position.x;
			old_pos = x;
			color = GameManager.Instance.getColor(player);
			animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString() + "/animation/" + color.ToString() + "_runner");
			animator = GetComponent<Animator>();			
			animator.runtimeAnimatorController = animCtrl;
		}else{
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if (!stop) {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (x + 50f, transform.position.y, transform.position.z), 7 * Time.deltaTime);

			if(transform.position.x-old_pos>0.5f)
				animator.SetBool("isMoving", true);
			else
				animator.SetBool("isMoving", false);

			old_pos = transform.position.x;
		}
	}

	private void move() {
		if (!finished) {
			x = transform.position.x;
			stop = false;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Target") {
			finished = true;
			stop = true;
			int pos = sceneManager.Score(player);
			if(pos != GameManager.Instance.getNumPlayer() - 1)
				animator.SetBool("isWinner", true);
			else
				animator.SetBool("isLoser", true);
		}
	}
}
