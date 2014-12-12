﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BucketLevelManager : MonoBehaviour {
	
	private GameManager.eLevels level = GameManager.eLevels.Bucket;
	
	public int num_players = 4;
	Dictionary<GameManager.ePlayers, int> points = new Dictionary<GameManager.ePlayers, int>();
	public Text[] text_score;
	public GameObject panel_GameOver;
	public Text UIgold;
	public Text UIsilver;
	public Text UIbronze;
	public GameObject panel_Scores;
	public Text[] UIpts;
	
	public float seconds = 30;
	private bool finished;
	
	void Start() {
		Debug.Log("BUCKET LEVEL");
		finished = false;
		panel_GameOver.SetActive(false);
		panel_Scores.SetActive(false);
		if(GameManager.Instance.getNumPlayer()==0) {
			GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
			GameManager.Instance.startGame(num_players);
		}
		num_players = GameManager.Instance.getNumPlayer();

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
		int pos = 1;
		foreach (KeyValuePair<GameManager.ePlayers, int> pair in player)
		{
			Debug.Log(pair.Key + " POINTS " + pair.Value);
			switch(pos) {
			case 1: GameManager.Instance.addGold(pair.Key); UIgold.text = pair.Key.ToString(); break;
			case 2: GameManager.Instance.addSilver(pair.Key); UIsilver.text = pair.Key.ToString(); break;
			case 3: GameManager.Instance.addBronze(pair.Key); UIbronze.text = pair.Key.ToString(); break;
			}
			pos++;
		}
		StartCoroutine(GameOver());
	}
	
	public void score(GameManager.ePlayers player) {
		if(!finished){
			points[player]++;
			text_score[player.GetHashCode()].text = points[player].ToString();
			UIpts[player.GetHashCode()].text = points[player].ToString();
			Debug.Log(player + " score " + points[player]);
		}
	}
	
	IEnumerator GameOver() {
		Debug.Log("GAME OVER");
		finished = true;
		panel_GameOver.SetActive(true);
		yield return new WaitForSeconds(10f);
		panel_GameOver.SetActive(false);
		panel_Scores.SetActive(true);
		yield return new WaitForSeconds(10f);
		GameManager.Instance.gameOver(this.level);
	}
}
