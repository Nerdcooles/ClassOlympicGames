﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class NerdLevelManager : MonoBehaviour {
	
	private int num_players;
	private LevelManager lvm;
	private float[] distances;
	float[] times;

	private int finished = 0;
	
	void Awake() {
		if (MusicManager.Instance.Source.clip != Resources.Load<AudioClip> (MusicManager.songs [1])) {
			MusicManager.Instance.Source.Stop ();
			MusicManager.Instance.Source.clip = Resources.Load<AudioClip> (MusicManager.songs [1]);
			MusicManager.Instance.Source.Play ();
		}
	}
	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		distances = new float[num_players];
		times = new float[num_players];

	}
	
	public void Score(GameManager.ePlayers player, float distance) {
		finished++;
		distances [player.GetHashCode()] = distance;
		times[player.GetHashCode()] = Time.time;

		if (finished == num_players)
			Finish ();
	}
	
	void Finish() {
		CheckPodium ();
		lvm.FinishGame ();
	}
	
	void CheckPodium() {
		for(int player=0; player<num_players; player++) {
			float max = distances.Max ();
			Debug.Log("Max: " + max);
			
			if(max == 0)
				return;
			
			float min_time = Time.time+1;
			Debug.Log("Min time: " + min_time);
			GameManager.ePlayers winner = GameManager.ePlayers.none;
			for (int i=0; i<distances.Length; i++){
				Debug.Log("Check distance " + distances [i] + " at " + times[i] + "(min time is " + min_time + ")");
				if (distances [i] == max && times [i] < min_time) {
					Debug.Log("GOOD " + ((GameManager.ePlayers)i).ToString());
					winner = (GameManager.ePlayers)i;
					min_time = times[i];
				}
			}
			if(winner!=GameManager.ePlayers.none) {
				distances[winner.GetHashCode()] = -2000;
				times[winner.GetHashCode()] = -1;
				lvm.setPodium(winner, player);
			}
		}
	}

}
