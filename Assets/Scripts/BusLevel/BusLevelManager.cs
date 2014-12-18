using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BusLevelManager : LevelManager {
		
	private int player_pos;
	private float start_time;
	private float finish_time;
		
	void Awake() {
		player_pos = 0;
		start_time = 0;
		finish_time = 0;
	}

	void Start() {
		level = GameManager.eLevels.Bus;
		_start(level);
		Debug.Log("BUS LEVEL");
	}

	public int Finish(GameManager.ePlayers player) {
		finish_time = Time.time - start_time;
		levelUI.score(player, finish_time.ToString("0.00") + " sec");
		if(player_pos < 3){
			GameManager.Instance.addMedal(player, (GameManager.eMedals)player_pos);
			levelUI.medal(player, (GameManager.eMedals)player_pos);
		}
		player_pos++;
		if(player_pos >= num_players)
			StartCoroutine(GameOver());
		return player_pos-1;
	}

	protected override void LetsGo() {
		start = true;
		start_time = Time.time;
	}
}
