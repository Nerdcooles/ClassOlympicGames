using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BusLevelManager : MonoBehaviour {
	
	private GameManager.eLevels level = GameManager.eLevels.Bus;

	public int num_players = 4;
	public GameObject panel_GameOver;
	public Text UIgold;
	public Text UIsilver;
	public Text UIbronze;

	private int player_pos;
		
	void Start() {
		Debug.Log("BUS LEVEL");
		panel_GameOver.SetActive(false);
		if(GameManager.Instance.getNumPlayer()==0) {
			GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
			GameManager.Instance.startGame(num_players);
		}
		num_players = GameManager.Instance.getNumPlayer();
		player_pos = 1;
	}

	public void Finish(GameManager.ePlayers player) {
		Debug.Log(player + " position " + player_pos);
		switch(player_pos) {
		case 1: GameManager.Instance.addGold(player); UIgold.text = player.ToString(); break;
		case 2: GameManager.Instance.addSilver(player); UIsilver.text = player.ToString(); break;
		case 3: GameManager.Instance.addBronze(player); UIbronze.text = player.ToString(); break;
		}
		player_pos++;
		if(player_pos > num_players)
			StartCoroutine(GameOver());
	}

	IEnumerator GameOver() {
		Debug.Log("GAME OVER");
		panel_GameOver.SetActive(true);
		yield return new WaitForSeconds(10f);
		GameManager.Instance.gameOver(this.level);
	}
}
