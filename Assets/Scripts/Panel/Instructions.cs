using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TouchScript.Gestures;

public class Instructions : MonoBehaviour {

	LevelManager lvm;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
	}
	
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey))
			lvm.SkipInstructions();
	}

}
