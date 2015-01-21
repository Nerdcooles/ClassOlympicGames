using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : Panel {
	
	private int initial_countdown = 3;
	private Sprite[] sprite;

	protected override void PrepareToShow() {
		sprite = new Sprite[4];
		for(int i=0; i<4; i++) 
			sprite[i] = Resources.Load <Sprite> ("Sprites/Common/countdown_" + i);
		InvokeRepeating ("CountDown", 0.1f, 0.8f);
	}
	
	private void CountDown() {
		if(initial_countdown<0) {
			img.enabled = false;
			lvm.StartGame();
			CancelInvoke("CountDown");
			return;
		}
		img.sprite = sprite[initial_countdown];
		initial_countdown--;
	}

}
