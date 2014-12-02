using System;
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
	
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<BusLevelManager>() as BusLevelManager;
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
		left_btn.GetComponent<TapGesture>().Tapped -= tapLeft;
		right_btn.GetComponent<TapGesture>().Tapped -= tapRight;
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
		Vector2 pos = transform.position;
		pos.x += deltaX;
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D other) {
		levelManager.Finish(this.player);
		Destroy(gameObject);
	}
}
