using System.Collections.Generic;

public class Player{

	GameManager.eColors color;
	GameManager.ePlayers number;

	Dictionary<GameManager.eMedals, int> medals = new Dictionary<GameManager.eMedals, int>();

	public Player() {
		for(int i=0; i<3; i++) {
			medals.Add((GameManager.eMedals)i, 0); 
		}
	}

	public void setColor(GameManager.eColors color) {
		this.color = color;
	}

	public GameManager.eColors getColor() {
		return color;
	}

	public void addMedal(GameManager.eMedals medal) {
		int oldMedals;
		medals.TryGetValue(medal, out oldMedals); 
		medals[medal] = oldMedals + 1;	
	}

	public int getMedals(GameManager.eMedals medal) {
		int num;
		medals.TryGetValue(medal, out num); 	
		return num;
	}

	public GameManager.ePlayers Number {
		get {
			return number;
		}
		set {
			number = value;
		}
	}
}
