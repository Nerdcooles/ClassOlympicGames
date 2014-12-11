using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

	public enum ePlayers {p01, p02, p03, p04};
	public enum eGameMode {CLASSIC, TRAINING};
	public enum eLevels {Bus, Bucket, Archery};
	
	Dictionary<ePlayers, Player> players = new Dictionary<ePlayers, Player>();
	private eGameMode gameMode;
		
	protected GameManager () {} // guarantee this will be always a singleton only - can't use the constructor!

	public void startMode(eGameMode mode) {
		this.gameMode = mode;
	}

	public void startGame(int num_players) {
		Debug.Log("NEW GAME");
		players = new Dictionary<ePlayers, Player>();
		for(int i=0; i<num_players; i++)
			players.Add((ePlayers)i, new Player());
	}

	public int getNumPlayer() {
		return players.Count;
	}

	public void addGold(ePlayers player) {
		if (players.ContainsKey(player))
			players[player].addGold();
	}
	
	public void addSilver(ePlayers player) {
		if (players.ContainsKey(player))	
			players[player].addSilver();
	}

	public void addBronze(ePlayers player) {
		if (players.ContainsKey(player))
			players[player].addBronze();
		
	}

	public void gameOver(eLevels level) {
		if(gameMode == eGameMode.CLASSIC) {
			switch(level) {
			case eLevels.Bus: 		Application.LoadLevel("Bucket"); break;
			case eLevels.Bucket: 	Application.LoadLevel("Archery"); break;
			case eLevels.Archery: 	Application.LoadLevel("Menu"); break;
				
			}
		}else{
			Application.LoadLevel("Menu");
		}
			
	}
}
