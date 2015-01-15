using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

	public enum ePlayers {p01, p02, p03, p04, none};
	public enum eMedals {Gold, Silver, Bronze};
	public enum eGameMode {CLASSIC, TRAINING};
	public enum eLevels {Home, Delaybus, Bucketball, Arteachery, SkipTheTest, LongboardJump, Nerdthrow , Award};
	public enum eColors {blue, green, red, yellow};
	
	Dictionary<ePlayers, Player> players = new Dictionary<ePlayers, Player>();
	private eGameMode gameMode;
	private eLevels level = eLevels.Home;
		
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

	public List<ePlayers> getWinners() {
		List<Player> playersToCtrl = new List<Player>();
		List<ePlayers> winners = new List<ePlayers> ();

		//populate list to control
		for (int i=0; i<players.Count; i++)
			playersToCtrl.Add (players [(ePlayers)i]);


		while (playersToCtrl.Count > 0) {
			ePlayers winner = WhoHasMoreMedals(playersToCtrl, eMedals.Gold).Number;
			winners.Add(winner);
			playersToCtrl.Remove(players[winner]);
		}

		return winners;
	}

	Player WhoHasMoreMedals(List<Player> playersToCtrl, eMedals medal) {
		//find max golds value
		int max_medals = 0;
		foreach (Player p in playersToCtrl) {
			int medals = p.getMedals (medal);
			if (medals > max_medals) {
				max_medals = medals;
			}
		}

		//store players with max golds value
		List<Player> results = new List<Player>();
		foreach (Player p in playersToCtrl) {
			if (p.getMedals (medal) == max_medals) {
				results.Add(p);
			}
		}

		if(results.Count > 1) {
			//if more then one, repeat
			return WhoHasMoreMedals(results, (eMedals)(medal.GetHashCode()+1));
		}else{
			return results[0];
		}
	}

	public void addMedal(ePlayers player, eMedals medal) {
		if (players.ContainsKey (player) && medal.GetHashCode() != 3) {
						players [player].addMedal (medal);
				}
	}

	public int getMedal(ePlayers player, eMedals medal) {
		if (players.ContainsKey(player))
			return players[player].getMedals(medal);
		return 0;
	}

	public bool IsPlaying(ePlayers player) {
		if (players.ContainsKey (player)) {
						return true;
				} else {
						return false;
				}
		}

	public void startMode(eGameMode mode) {
		level = eLevels.Home;
		this.gameMode = mode;
	}

	public void createPlayers(int num_players) {
		players = new Dictionary<ePlayers, Player>();
		
		for(int i=0; i<num_players; i++){
			Player p = new Player();
			p.Number = (ePlayers)i;
			players.Add((ePlayers)i, p);
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
		case eColors.blue: return new Color(93/255f,93/255f,244/255f,255/255f); break;
		case eColors.red: return new Color(255/255f,68/255f,68/255f,255/255f); break;
		case eColors.yellow: return new Color(246/255f, 229/255f, 38/255f,255/255f); break;
		case eColors.green: return new Color(111/255f,255/255f,11/255f,255/255f); break;
		default: throw new System.Exception("Player get color error");
		}
	}

	public void LevelOver() {
		MenuManager.LevelOver ();
	}

	public eLevels Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}

}
