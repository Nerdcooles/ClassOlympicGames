using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LongLevelManager : MonoBehaviour {
		
	private int num_players;
	private LevelManager lvm;
	private List<GameManager.ePlayers> notClassified;
	private float[] distances;
	private int finished = 0;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		distances = new float[num_players];
		notClassified = new List<GameManager.ePlayers> ();
	}
	
	public void Score(GameManager.ePlayers player, float distance) {
		finished++;
		if (distance == 0)
						notClassified.Add (player);
				else
						distances [player.GetHashCode()] = distance;
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

		for(int podium=0; podium < 3 && podium < num_players; podium++) {
			float max = distances.Max ();

			if(max == 0) {
					return;
			}
				for (int player=0; player<distances.Count(); player++) {

						if(distances[player]==max){
							distances[player] = -1;
							lvm.setPodium((GameManager.ePlayers)player, podium);
						}
				 }
		 }
	}

}

