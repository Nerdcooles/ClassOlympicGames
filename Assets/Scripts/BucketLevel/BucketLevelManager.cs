using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BucketLevelManager : MonoBehaviour {
		
	int[] points;

	public int seconds = 10;
	private int num_players;
	private LevelManager lvm;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();

		//init points
		points = new int[num_players];
		for(int i=0; i<num_players; i++) {
			points[i] = 0; 
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
		InvokeRepeating ("Timer", 0.1f, 1);

	}

	void Timer() {
		seconds--;
		if(seconds < 0){
			Finished ();
			CancelInvoke("Timer");
		}
	}

	void Finished() {
		List<GameManager.ePlayers> firstPlace = new List<GameManager.ePlayers> ();
		int max = points.Max ();
		if (max != -1) {
						Debug.Log ("first " + max);
						for (int i=0; i<points.Length; i++)
								if (points [i] == max) {
										Debug.Log ("P0" + (i + 1));
										firstPlace.Add ((GameManager.ePlayers)i);
										points [i] = -1;
								}
				}
		lvm.setFirstPlace (firstPlace);

		List<GameManager.ePlayers> secondPlace = new List<GameManager.ePlayers> ();
		max = points.Max ();
		if (max != -1) {
						Debug.Log ("second " + max);
						for (int i=0; i<points.Length; i++)
								if (points [i] == max) {
										Debug.Log ("P0" + (i + 1));
										secondPlace.Add ((GameManager.ePlayers)i);
										points [i] = -1;
								}
				}
		lvm.setSecondPlace (secondPlace);

		List<GameManager.ePlayers> thirdPlace = new List<GameManager.ePlayers> ();
		max = points.Max ();
		if (max != -1) {
						Debug.Log ("third " + max);
						for (int i=0; i<points.Length; i++)
								if (points [i] == max) {
										Debug.Log ("P0" + (i + 1));
										thirdPlace.Add ((GameManager.ePlayers)i);
										points [i] = -1;
								}
				}
		lvm.setThirdPlace (thirdPlace);

		lvm.FinishGame();
	}

	public void Score(GameManager.ePlayers player) {
		points[player.GetHashCode()]++;
		Debug.Log (player.ToString () + " " + points [player.GetHashCode ()]);
	}

}
