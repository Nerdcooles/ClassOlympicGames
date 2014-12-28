using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BucketLevelManager : LevelManager {
		
	Dictionary<GameManager.ePlayers, int> points = new Dictionary<GameManager.ePlayers, int>();
	
	public int seconds = 30;
	private bool finished;
	
	void Awake() {
		level = GameManager.eLevels.Bucket;
		PrepareLevel(level);
		Debug.Log("BUCKET LEVEL");
		finished = false;
		for(int i=0; i<num_players; i++) {
			points.Add((GameManager.ePlayers)i, 0); 
		}
	}

	void Start() {
		levelUI.show(LevelUI.ePanel.Scoreboard);
		levelUI.show(LevelUI.ePanel.Timer);
		levelUI.timer(seconds);
		StartCoroutine(LevelTimer(seconds));
	}

	IEnumerator LevelTimer(int waitTime) {
		yield return new WaitForSeconds(4f);
		for(int i=waitTime; i>=0; i--){
			levelUI.timer(i);
			yield return new WaitForSeconds(1f);
		}
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
			levelUI.score(player, points[player].ToString());
		}
	}
}
