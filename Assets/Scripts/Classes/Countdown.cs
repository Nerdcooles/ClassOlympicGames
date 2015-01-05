using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	public GameObject[] countdown;

	private int initial_countdown = 3;
	private LevelManager lvm;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	public void StartCountdown() {
		InvokeRepeating ("CountDown", 0.1f, 1);
	}
	
	private void CountDown() {
		hide(initial_countdown+1);
		
		if(initial_countdown<0) {
			lvm.StartGame();
			CancelInvoke("CountDown");
		}
		
		show(initial_countdown);
		initial_countdown--;
	}

	private void show(int sec) {
		try {
		countdown[sec].SetActive(true);
		} catch { }
	}
	
	private void hide(int sec) {
		try {
		countdown[sec].SetActive(false);
		} catch { }
	}
}
