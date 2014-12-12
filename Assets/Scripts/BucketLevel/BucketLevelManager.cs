using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BucketLevelManager : LevelManager {
		
	Dictionary<GameManager.ePlayers, int> points = new Dictionary<GameManager.ePlayers, int>();
	public Text[] text_score;
	
	public float seconds = 30;
	private bool finished;
	
	void Start() {
		_start();
		Debug.Log("BUCKET LEVEL");
		level = GameManager.eLevels.Bucket;
		finished = false;
		for(int i=0; i<num_players; i++) {
			points.Add((GameManager.ePlayers)i, 0); 
		}
		StartCoroutine(Countdown(seconds));
	}

	IEnumerator Countdown(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		var player = from pair in points
			orderby pair.Value descending
				select pair;
		int player_pos = 0;
		foreach (KeyValuePair<GameManager.ePlayers, int> pair in player)
		{
			if(player_pos < 3){
				GameManager.Instance.addMedal(pair.Key, (GameManager.eMedals)player_pos);
				levelUI.medal(pair.Key, (GameManager.eMedals)player_pos);
			}else {
				break;
			}
			player_pos++;
		}
		finished = true;
		StartCoroutine(GameOver());
	}
	
	public void score(GameManager.ePlayers player) {
		if(!finished){
			points[player]++;
			text_score[player.GetHashCode()].text = points[player].ToString();
			levelUI.score(player, points[player].ToString());
		}
	}
}
