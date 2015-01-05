using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	public float deltaX = 1;
	public GameManager.ePlayers player;

	public GameObject button;
	private BusLevelManager levelManager;
	private Animator animator;
	private bool finished;
	private int last;

	void Awake() {
		levelManager = GameObject.Find("LevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
	}

	void Start () {
		animator = GetComponent<Animator>();
		finished = false;
		last = GameManager.Instance.getNumPlayer();
	}

	private void OnEnable()
	{
		button.GetComponent<PressGesture>().Pressed += move;
	}
	
	private void OnDisable()
	{
		try{
			button.GetComponent<PressGesture>().Pressed -= move;
		}catch{}
	}

	private void move(object sender, EventArgs e) {
		Debug.Log(player.ToString() + " tap");
//		if(levelManager.isStarted() && !finished) {
			StopAllCoroutines();
			Vector3 pos = transform.position;
			pos.x += deltaX;
			transform.position = pos;
			animator.SetBool("isMoving", true);
			StartCoroutine(animation());
//		}
	}

	private IEnumerator animation() {
		yield return new WaitForSeconds(0.5f);
		animator.SetBool("isMoving",false);
	}

//	private void OnTriggerEnter2D(Collider2D other) {
//		int pos = levelManager.Finish(this.player)+1;
//		if(pos!=last || last==1) animator.SetBool("isWinner", true);
//		else animator.SetBool("isLoser", true);
//		finished = true;
//	}
}
