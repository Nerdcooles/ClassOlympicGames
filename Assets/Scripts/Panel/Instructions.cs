﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TouchScript.Gestures;

public class Instructions : MonoBehaviour {

	LevelManager lvm;
	private bool canSkip = false;
	private int secToSkip = 2;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		InvokeRepeating ("WaitToSkip", 0.1f, 1f);
		if(lvm.State != LevelManager.eState.Instructions)
			canSkip = true;
	}
	
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip) {
			lvm.SkipInstructions();
			Destroy(gameObject);
		}

	}

	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			canSkip=true;
			CancelInvoke("WaitToSkip");
			
		}
	}

}
