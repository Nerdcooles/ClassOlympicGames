using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Utils;
using System;

public class PanButton : MonoBehaviour {

	public float y_top = -2.4f;
	public float y_bottom = -4.3f;

	private Vector3 position;
	private float pos_x;
	private float direction;
	
	void Start()
	{
		position = transform.position;
		pos_x = position.x;
	}
	
	private void OnEnable()
	{
		gameObject.GetComponent<PanGesture>().StateChanged += onStateChangeHandler;
		gameObject.GetComponent<ReleaseGesture>().Released += shoot;
	}
	
	private void OnDisable()
	{
		try{
			gameObject.GetComponent<PanGesture>().StateChanged -= onStateChangeHandler;
			gameObject.GetComponent<ReleaseGesture>().Released -= shoot;
		}catch{ }
	}
	
	private void onStateChangeHandler(object sender, EventArgs e)
	{
		position = transform.position;
		position.x = pos_x;
		if(position.y < y_top && position.y >y_bottom)
			direction = position.y;
		position.y = direction;
		transform.position = position;
		
		Debug.Log(position);	
	}

	private void shoot(object sender, EventArgs e)
	{
		Debug.Log("SHOOT");
	}
}

