using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
	
	private int initial_countdown = 3;
	private LevelManager lvm;
	private Image s_renderer;
	private Sprite[] sprite;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		s_renderer = gameObject.GetComponent<Image> ();
		sprite = new Sprite[4];
		for(int i=0; i<4; i++) 
			sprite[i] = Resources.Load <Sprite> ("Sprites/Common/countdown_" + i);
	}

	public void StartCountdown() {
		InvokeRepeating ("CountDown", 0.1f, 1);
	}
	
	private void CountDown() {
		s_renderer.enabled = true;

		if(initial_countdown<0) {
			s_renderer.enabled = false;
			lvm.StartGame();
			CancelInvoke("CountDown");
			return;
		}
		s_renderer.sprite = sprite[initial_countdown];
		initial_countdown--;
	}

}
