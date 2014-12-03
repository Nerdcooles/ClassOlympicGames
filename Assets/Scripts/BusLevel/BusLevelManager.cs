using UnityEngine;
using System.Collections;

public class BusLevelManager : MonoBehaviour {
	
	public int num_players = 4;
	int player_pos;
		
	void Start() {
		Debug.Log("BUS LEVEL");
		if(GameManager.Instance.getNumPlayer()==0)
			GameManager.Instance.startGame(num_players);
		num_players = GameManager.Instance.getNumPlayer();
		player_pos = 1;
	}

	public void Finish(GameManager.ePlayers player) {
		Debug.Log(player + " position " + player_pos);
		switch(player_pos) {
		case 1: GameManager.Instance.addGold(player); break;
		case 2: GameManager.Instance.addSilver(player); break;
		case 3: GameManager.Instance.addBronze(player); break;
		}
		player_pos++;
		if(player_pos > num_players)
			GameOver();
	}

	private void GameOver() {
		Debug.Log("GAME OVER");
		GameManager.Instance.printMedals();
		Application.LoadLevel("Bucket");
	}
}
