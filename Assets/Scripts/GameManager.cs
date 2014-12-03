using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : Singleton<GameManager> {

	public enum ePlayers {p01, p02, p03, p04};
	
	Dictionary<ePlayers, Player> players = new Dictionary<ePlayers, Player>();
	private int num;
		
	protected GameManager () {} // guarantee this will be always a singleton only - can't use the constructor!

	public void startGame(int num_players) {
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

	public void printMedals() {
		var list = players.Keys.ToList();
		foreach(ePlayers p in list)
			Debug.Log(p + " GOLD " + players[p].getGold() + " SILVER " + players[p].getSilver() + " BRONZE " + players[p].getBronze());
	}
}
