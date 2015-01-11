using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class MenuManager {

	public static void levelOver(GameManager.eLevels level) {
		if (GameManager.Instance.getGameMode () == GameManager.eGameMode.CLASSIC)
						Application.LoadLevel (((GameManager.eLevels)(level.GetHashCode () + 1)).ToString ());
				else
						newGame ();
	}

	public static void newGame() {
		Application.LoadLevel("SelectMode");
	}

	public static void startHome() {
		Application.LoadLevel("Home");
	}
	
	public static void selectNumber() {
		Application.LoadLevel("SelectNumber");
	}

	public static void selectMode() {
		Application.LoadLevel("SelectMode");
	}

	public static void selectLevel() {
		Application.LoadLevel("SelectLevel");
	}
	
	public static void selectPlayer() {
		Application.LoadLevel("SelectPlayer");
	}

	public static void credits() {
		Debug.Log("credits");
	}
	
	public static void startGame() {
		Application.LoadLevel(((GameManager.eLevels)0).ToString());
	}
	
	public static void startLevel(GameManager.eLevels level) {
		Application.LoadLevel (((GameManager.eLevels)level.GetHashCode ()).ToString ());
	}
}
