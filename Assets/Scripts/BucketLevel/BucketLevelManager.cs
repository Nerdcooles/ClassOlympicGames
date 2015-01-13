using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BucketLevelManager : MonoBehaviour {
		
	int[] points;
	float[] times;

	private const int START_SEC = 30;

	private int seconds;
	private int num_players;
	private LevelManager lvm;
	private Timebar timebar;

	private Text[] scoreP;

	void Start() {
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

		scoreP = new Text[4];
		for(int i=0; i<4; i++) {
			scoreP[i] = GameObject.Find("ScoreP"+(i+1)).GetComponent<Text>();
			if(i<num_players) {
				scoreP[i].text = "0";
				scoreP[i].color = GameManager.Instance.getSysColor((GameManager.ePlayers)i);
			}else
				scoreP[i].gameObject.SetActive(false);

		}

		switch (num_players) {
		case 1: scoreP[0].GetComponent<RectTransform>().position = new Vector3(-164, 251, 0);

			break;
		case 2: scoreP[0].GetComponent<RectTransform>().position = scoreP[1].GetComponent<RectTransform>().position;
			scoreP[1].GetComponent<RectTransform>().position = scoreP[2].GetComponent<RectTransform>().position;

			break;
		case 3: //scoreP0 nothing
			scoreP[1].GetComponent<RectTransform>().position = new Vector3(-164, 251, 0);
			scoreP[2].GetComponent<RectTransform>().position = scoreP[3].GetComponent<RectTransform>().position;
			break;
				}

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
		float perc = (START_SEC - seconds) / (float)START_SEC;
		timebar.setPercentage (perc);
		Debug.Log (seconds);
		seconds--;
		if(seconds < 0){
			Finished ();
			CancelInvoke("Timer");
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
		int pts = 		++points[player.GetHashCode()];
		scoreP [player.GetHashCode ()].text = pts.ToString();
		times[player.GetHashCode()] = Time.time;
	}

}
