using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LongLevelManager : MonoBehaviour {
		
	private int num_players;
	private LevelManager lvm;
	private List<GameManager.ePlayers> notClassified;
	private float[] distances;
	float[] times;
	private int finished = 0;

	void Start() {
		if (MusicManager.Instance.Source.clip != Resources.Load<AudioClip> (MusicManager.songs [0])) {
						MusicManager.Instance.Source.Stop ();
						MusicManager.Instance.Source.clip = Resources.Load<AudioClip> (MusicManager.songs [0]);
						MusicManager.Instance.Source.Play ();
				}
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		distances = new float[num_players];
		times = new float[num_players];

		notClassified = new List<GameManager.ePlayers> ();
	}
	
	public void Score(GameManager.ePlayers player, float distance) {
		finished++;
		if (distance == 0)
						notClassified.Add (player);
				else {
						distances [player.GetHashCode()] = distance;
			times[player.GetHashCode()] = Time.time;

		}
		if (finished == num_players)
						Finish ();
	}

	void Finish() {
		if (notClassified.Count == num_players)
						Debug.Log ("NO BODY CLASSIFIED");
				else
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
				distances[winner.GetHashCode()] = -1;
				times[winner.GetHashCode()] = -1;
				lvm.setPodium(winner, player);
			}
		}
	}

}

