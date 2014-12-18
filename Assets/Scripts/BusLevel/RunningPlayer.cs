using System;
using System.Collections;
using UnityEngine;
using TouchScript.Gestures;

public class RunningPlayer : MonoBehaviour {

	public GameObject left_btn;
	public GameObject right_btn;

	public float deltaX = 1;
	public GameManager.ePlayers player;

	bool left_pressed = true;
	bool right_pressed = false;

	private BusLevelManager levelManager;
	private Animator animator;
	private bool finished;
	private int last;

	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
		animator = GetComponent<Animator>();
		finished = false;
		last = GameManager.Instance.getNumPlayer();
	}

	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		left_btn.GetComponent<TapGesture>().Tapped += tapLeft;
		right_btn.GetComponent<TapGesture>().Tapped += tapRight;
		
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
		left_btn.GetComponent<TapGesture>().Tapped -= tapLeft;
		right_btn.GetComponent<TapGesture>().Tapped -= tapRight;
		}catch{}
	}

	private void tapLeft(object sender, EventArgs e)
	{
		if(right_pressed){
			move();
			right_pressed = false;
			left_pressed = true;
		}
	}

	private void tapRight(object sender, EventArgs e)
	{
		if(left_pressed){
			move();
			right_pressed = true;
			left_pressed = false;
		}
	}

	private void move() {
		if(levelManager.isStarted() && !finished) {
			StopAllCoroutines();
			Vector3 pos = transform.position;
			pos.x += deltaX;
			transform.position = pos;
			animator.SetBool("isMoving", true);
			StartCoroutine(animation());
		}
	}

	private IEnumerator animation() {
		yield return new WaitForSeconds(0.5f);
		animator.SetBool("isMoving",false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		int pos = levelManager.Finish(this.player)+1;
		if(pos!=last || last==1) animator.SetBool("isWinner", true);
		else animator.SetBool("isLoser", true);
		finished = true;
	}
}
