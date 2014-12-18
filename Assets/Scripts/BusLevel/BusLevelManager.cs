using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BusLevelManager : LevelManager {
		
	private int player_pos;
	private float start_time;
	private float finish_time;
		
	void Start() {
		level = GameManager.eLevels.Bus;
		_start(level);
		Debug.Log("BUS LEVEL");
		player_pos = 0;
		start_time = Time.time;
	}

	public void Finish(GameManager.ePlayers player) {
		finish_time = Time.time - start_time - 3;
		levelUI.score(player, finish_time.ToString("0.00"));
		if(player_pos < 3){
			GameManager.Instance.addMedal(player, (GameManager.eMedals)player_pos);
			levelUI.medal(player, (GameManager.eMedals)player_pos);
		}
		player_pos++;
		if(player_pos >= num_players)
			StartCoroutine(GameOver());
	}
}
