using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

	public enum ePlayers {p01, p02, p03, p04};
	public enum eMedals {Gold, Silver, Bronze};
	public enum eGameMode {CLASSIC, TRAINING};
	public enum eLevels {Bus, Bucket, Archery};
	
	Dictionary<ePlayers, Player> players = new Dictionary<ePlayers, Player>();
	private eGameMode gameMode;
		
	protected GameManager () {} // guarantee this will be always a singleton only - can't use the constructor!

	public eGameMode getGameMode() {
		return gameMode;
	}

	public int getNumPlayer() {
		return players.Count;
	}

	public void addMedal(ePlayers player, eMedals medal) {
		if (players.ContainsKey(player))
			players[player].addMedal(medal);
	}

	public int getMedal(ePlayers player, eMedals medal) {
		if (players.ContainsKey(player))
			return players[player].getMedals(medal);
		return 0;
	}

	public string getName(ePlayers player) {
		if (players.ContainsKey(player))
			return players[player].getName();
		return "";
	}

	public void gameOver(eLevels level) {
		if(gameMode == eGameMode.CLASSIC) {
			switch(level) {
			case eLevels.Bus: 		Application.LoadLevel("Bucket"); break;
			case eLevels.Bucket: 	Application.LoadLevel("NewGame"); break;
			case eLevels.Archery: 	Application.LoadLevel("NewGame"); break;	
			}
		}else{
			newGame();
		}	
	}

	public void newGame() {
		Application.LoadLevel("NewGame");
	}

	public void startMode(eGameMode mode) {
		this.gameMode = mode;
	}
	
	public void selectPlayers() {
		Application.LoadLevel("SelectPlayers");
	}
	
	public void startGame(int num_players) {
		players = new Dictionary<ePlayers, Player>();

		for(int i=0; i<num_players; i++){
			Player _player = new Player();
			_player.setName("Player " + i);
			players.Add((ePlayers)i, _player);
		}

		switch(gameMode) {
		case eGameMode.CLASSIC: Application.LoadLevel("Bus"); break;
		case eGameMode.TRAINING: Application.LoadLevel("SelectLevel"); break;
		}
	}
	
	public void startLevel(eLevels level) {
		switch(level) {
		case eLevels.Bus: Application.LoadLevel("Bus"); break;
		case eLevels.Bucket: Application.LoadLevel("Bucket"); break;
		}
	}
}
