using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ArcheryLevelManager : MonoBehaviour {
	
	int[] points;
	float[] times;
	private const float START_SEC = 30;

	private float seconds;
	private int num_players;
	private LevelManager lvm;
	private Timebar timebar;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		timebar = GameObject.Find ("Timebar").GetComponent<Timebar> ();
		seconds = START_SEC;

		//init points
		points = new int[num_players];
		times = new float[num_players];
		
		for(int i=0; i<num_players; i++) {
			points[i] = 0; 
			times[i] = 0; 
		}
	}
	void OnEnable()
	{
		lvm.OnStart += StartTimer;
	}
	
	
	void OnDisable()
	{
		lvm.OnStart -= StartTimer;
	}
	
	void StartTimer() {
		InvokeRepeating ("Timer", 0.1f, 0.1f);
	}
	
	void Timer() {
		float perc = (START_SEC - seconds) / (float)START_SEC;
		timebar.setPercentage (perc);
		seconds -= 0.1f;
		if(seconds <= 0){
			Finished ();
			CancelInvoke("Timer");
			return;
		}
	}
	
	void Finished() {
		for(int pos=0; pos<num_players; pos++) {
			int max = points.Max ();
			float min_time = Time.time;
			GameManager.ePlayers winner = (GameManager.ePlayers)pos;
			for (int i=0; i<points.Length; i++){
				if (points [i] == max && times [i] < min_time) {
					winner = (GameManager.ePlayers)i;
					min_time = times[i];
				}
			}
			points[winner.GetHashCode()] = -1;
			times[winner.GetHashCode()] = -1;
			Debug.Log(winner.ToString() + " position " + pos);
			lvm.setPodium(winner, pos);
		}
		
		lvm.FinishGame();
	}
	
	public void Score(GameManager.ePlayers player) {
		points[player.GetHashCode()]++;
		times[player.GetHashCode()] = Time.time;
	}

}
