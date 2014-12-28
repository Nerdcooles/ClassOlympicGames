using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class MenuManager {

	public static void gameOver(GameManager.eLevels level) {
		if(GameManager.Instance.getGameMode() == GameManager.eGameMode.CLASSIC) {
			switch(level) {
			case GameManager.eLevels.Bucket: 	Application.LoadLevel("Bus"); break;
			case GameManager.eLevels.Bus: 		Application.LoadLevel("Archery"); break;
			case GameManager.eLevels.Archery: 	Application.LoadLevel("SelectMode"); break;	
			}
		}else{
			newGame();
		}	
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
	
	public static void selectPlayer(int num_players) {
		GameManager.Instance.createPlayers(num_players);
		Application.LoadLevel("SelectPlayer");
	}

	public static void credits() {
		Debug.Log("credits");
	}
	
	public static void startGame() {
		switch(GameManager.Instance.getGameMode()) {
		case GameManager.eGameMode.CLASSIC: Application.LoadLevel("Bucket"); break;
		case GameManager.eGameMode.TRAINING: Application.LoadLevel("Bucket"); break;
		}
	}
	
	public static void startLevel(GameManager.eLevels level) {
		switch(level) {
		case GameManager.eLevels.Bus: Application.LoadLevel("Bus"); break;
		case GameManager.eLevels.Bucket: Application.LoadLevel("Bucket"); break;
		case GameManager.eLevels.Archery: Application.LoadLevel("Archery"); break;
		}
	}
}
