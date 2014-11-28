using UnityEngine;

public class GameManager : Singleton<GameManager> {

	private int num_player = 2;

	Player Player1 = new Player(ePlayers.p01);
	Player Player2 = new Player(ePlayers.p02);
	Player Player3 = new Player(ePlayers.p03);
	Player Player4 = new Player(ePlayers.p04);
	
	public enum ePlayers {p01, p02, p03, p04};
	
	protected GameManager () {} // guarantee this will be always a singleton only - can't use the constructor!


	public int getNumPlayer() {
		return num_player;
	}

	public void addGold(ePlayers player) {
		switch(player) {
		case ePlayers.p01: Player1.addGold(); break;
		case ePlayers.p02: Player2.addGold(); break;
		case ePlayers.p03: Player3.addGold(); break;
		case ePlayers.p04: Player4.addGold(); break;
		}
	}
	
	public void addSilver(ePlayers player) {
		switch(player) {
		case ePlayers.p01: Player1.addSilver(); break;
		case ePlayers.p02: Player2.addSilver(); break;
		case ePlayers.p03: Player3.addSilver(); break;
		case ePlayers.p04: Player4.addSilver(); break;
		}
	}

	public void addBronze(ePlayers player) {
		switch(player) {
		case ePlayers.p01: Player1.addBronze(); break;
		case ePlayers.p02: Player2.addBronze(); break;
		case ePlayers.p03: Player3.addBronze(); break;
		case ePlayers.p04: Player4.addBronze(); break;
		}
	}

	public void printMedals() {
		Debug.Log("PLAYER 1 G:" + Player1.getGold() + " S:" + Player1.getSilver() + " B:" +Player1.getBronze());
		Debug.Log("PLAYER 2 G:" + Player2.getGold() + " S:" + Player2.getSilver() + " B:" +Player2.getBronze());
		Debug.Log("PLAYER 3 G:" + Player3.getGold() + " S:" + Player3.getSilver() + " B:" +Player3.getBronze());
		Debug.Log("PLAYER 4 G:" + Player4.getGold() + " S:" + Player4.getSilver() + " B:" +Player4.getBronze());
	}
}
