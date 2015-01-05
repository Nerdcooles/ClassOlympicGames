using UnityEngine;
using System.Collections;
using System;
using TouchScript.Gestures;

public class Instructions : MonoBehaviour {

	private LevelManager lvm;

	private bool canSkip = false;
	private int secToSkip = 2;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Instructions/" + lvm.getLevel().ToString());
	}

	void Start() {
		gameObject.SetActive(true);
	}

	void Update() {
		if(Input.touchCount > 0 || Input.anyKey)
			ShowCountdown();
	}

	public void Show(){
		gameObject.SetActive (true);
		Debug.Log ("instruction show");
		InvokeRepeating ("WaitToSkip", 0.1f, 1);
	}

	public void Hide(){
		gameObject.SetActive (false);
	}

	private void ShowCountdown() {
		if(canSkip)
		lvm.ShowCountdown ();
	}

	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			Debug.Log ("instruction can skip");
			canSkip=true;
			CancelInvoke("WaitToSkip");

		}
	}

}
