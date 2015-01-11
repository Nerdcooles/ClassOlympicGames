using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

	public enum ePlayers {p01, p02, p03, p04};
	public enum eMedals {Gold, Silver, Bronze};
	public enum eGameMode {CLASSIC, TRAINING};
	public enum eLevels {Bus, Bucket, Archery};
	public enum eColors {blue, green, red, yellow};
	
	Dictionary<ePlayers, Player> players = new Dictionary<ePlayers, Player>();
	private eGameMode gameMode;
		
	protected GameManager () {} // guarantee this will be always a singleton only - can't use the constructor!

	public eGameMode getGameMode() {
		return gameMode;
	}

	public int getNumPlayer() {
		return players.Count ();
	}

	public List<ePlayers> getPlayers() {
		List<ePlayers> ps = new List<ePlayers>(players.Keys);
		return ps;
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

	public void startMode(eGameMode mode) {
		this.gameMode = mode;
	}

	public void createPlayers(int num_players) {
		players = new Dictionary<ePlayers, Player>();
		
		for(int i=0; i<num_players; i++){
			Player _player = new Player();
			_player.setName("Player " + (i+1));
			_player.setColor((eColors)i);
			players.Add((ePlayers)i, _player);
		}
	}

	public void setColor(ePlayers player, eColors color) {
		if (players.ContainsKey(player)){
			players[player].setColor(color);
		}
	}

	public eColors getColor(ePlayers player) {
		return players[player].getColor();
	}
	
	public Color getSysColor(ePlayers player) {
		switch(players[player].getColor()){
		case eColors.blue: return Color.blue; break;
		case eColors.red: return Color.red; break;
		case eColors.yellow: return Color.yellow; break;
		case eColors.green: return Color.green; break;
		default: return Color.blue;
		}
	}

	public void LevelOver(eLevels level) {
		MenuManager.levelOver (level);
	}
}
