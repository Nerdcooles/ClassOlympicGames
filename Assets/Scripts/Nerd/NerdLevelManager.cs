using UnityEngine;
using System.Collections;
using System.Linq;

public class NerdLevelManager : MonoBehaviour {
	
	private int num_players;
	private LevelManager lvm;
	private float[] distances;
	private int finished = 0;

	void Awake() {
		MusicManager.Instance.Source.Stop ();
		MusicManager.Instance.Source.clip = Resources.Load<AudioClip>(MusicManager.songs[1]);
		MusicManager.Instance.Source.Play ();
	}
	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		distances = new float[num_players];
	}
	
	public void Score(GameManager.ePlayers player, float distance) {
		finished++;
		distances [player.GetHashCode()] = distance;
		if (finished == num_players)
			Finish ();
	}
	
	void Finish() {
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
