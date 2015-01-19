using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TouchScript.Gestures;

public class Instructions : Panel {

	protected override void PrepareToShow() {
		img.sprite = Resources.Load <Sprite> ("Sprites/Instructions/" + lvm.getLevel().ToString());
	}

	protected override void Skip() {
		if(lvm.State == LevelManager.eState.Instructions)
			lvm.ShowCountdown();
		if(lvm.State == LevelManager.eState.Pause) {
			this.Hide();
			lvm.Pause.Show();
		}
	}

}
