using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using TouchScript.Utils;
using System;

public class PanButton : MonoBehaviour {

	public GameObject player;

	private Vector3 position;
	private float x;
	private float center;
	private float y_new;
	private float direction;
	
	private float range = 1;
	
	void Start()
	{
		x = transform.position.x;
		center = transform.position.y;
	}
	
	private void OnEnable()
	{
		gameObject.GetComponent<SimplePanGesture>().Panned += onStateChangeHandler;
		gameObject.GetComponent<ReleaseGesture>().Released += shoot;
	}
	
	private void OnDisable()
	{
		try{
			gameObject.GetComponent<SimplePanGesture>().Panned -= onStateChangeHandler;
			gameObject.GetComponent<ReleaseGesture>().Released -= shoot;
		}catch{ }
	}
	
	private void onStateChangeHandler(object sender, EventArgs e)
	{
		position = transform.position;
		position.x = x;
		y_new = transform.position.y;
		if(y_new < (center + range) && y_new > (center - range))
			direction = y_new;
		position.y = direction;
		transform.position = position;
		
		player.GetComponent<ArcheryPlayer>().setDirection(direction - center);
	}

	private void shoot(object sender, EventArgs e)
	{
		player.GetComponent<ArcheryPlayer>().shoot();
	}
}

