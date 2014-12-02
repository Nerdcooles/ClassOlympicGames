using System;
using UnityEngine;
using TouchScript.Gestures;

public class BucketPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	public GameObject shoot_btn;
	public GameObject ballPrefab;
	public int alpha = 500;
	
	private float force;
	private float press_time;
	private Vector3 direction;
	
	
	// Use this for initialization
	void Start () {
		direction = (Quaternion.AngleAxis(45, transform.forward) * transform.right) * alpha;
	}
	
	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		shoot_btn.GetComponent<PressGesture>().Pressed += startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released += shoot;
		
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		shoot_btn.GetComponent<PressGesture>().Pressed += startPower;
		shoot_btn.GetComponent<ReleaseGesture>().Released += shoot;
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void startPower(object sender, EventArgs e) {
		press_time = Time.time;		
	}

	private void shoot(object sender, EventArgs e) {
		force = Time.time - press_time;
		GameObject ballInstance = Instantiate(ballPrefab, transform.position, transform.rotation) as GameObject;
		ballInstance.GetComponent<BucketBall>().setPlayer(player);
		ballInstance.rigidbody2D.AddForce(direction * force);
	}
}
