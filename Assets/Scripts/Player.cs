using System.Collections.Generic;

public class Player{

	Dictionary<GameManager.eMedals, int> medals = new Dictionary<GameManager.eMedals, int>();
	string name;

	public Player() {
		name = "";
		for(int i=0; i<3; i++) {
			medals.Add((GameManager.eMedals)i, 0); 
		}
	}

	public void setName(string name) {
		this.name = "" + name;
	}
	
	public string getName() {
		return name;
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
