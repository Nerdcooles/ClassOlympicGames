using System.Collections.Generic;

public class Player{

	Dictionary<GameManager.eMedals, int> medals = new Dictionary<GameManager.eMedals, int>();
	
	public Player() {
		for(int i=0; i<3; i++) {
			medals.Add((GameManager.eMedals)i, 0); 
		}
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

}
