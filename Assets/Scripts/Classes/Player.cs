using System.Collections.Generic;

public class Player{

	const int gold_value = 4, silver_value = 2, bronze_value = 1;

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
	
	public int Points {
		get {
			return getMedals(GameManager.eMedals.Gold)*gold_value + getMedals(GameManager.eMedals.Silver)*silver_value + getMedals(GameManager.eMedals.Bronze)*bronze_value;
		}
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
