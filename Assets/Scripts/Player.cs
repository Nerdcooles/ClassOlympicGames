
public class Player{

	int gold;
	int silver;
	int bronze;

	public Player() {
		gold = 0;
		silver = 0;
		bronze = 0;
	}

	public void addGold() {
		gold++;
	}

	public void addSilver() {
		silver++;
	}

	public void addBronze() {
		bronze++;
	}

	public int getGold() {
		return gold;
	}
	
	public int getSilver() {
		return silver;
	}
	
	public int getBronze() {
		return bronze;
	}



}
