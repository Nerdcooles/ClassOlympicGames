using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class MenuManager {

	public static void LevelOver() {
		Debug.Log ("Finished level " + GameManager.Instance.Level.ToString ());
		GameManager.Instance.Level = ((GameManager.eLevels)(GameManager.Instance.Level.GetHashCode () + 1));
		Debug.Log ("Next level " + GameManager.Instance.Level.ToString ());

		if (GameManager.Instance.getGameMode () == GameManager.eGameMode.CLASSIC)
						Summary ();
		else
						NewGame ();
	}

	public static void NewGame() {
		Application.LoadLevel("Home");
	}

	public static void StartHome() {
		Application.LoadLevel("Home");
	}
	
	public static void SelectNumber() {
		Application.LoadLevel("SelectNumber");
	}

	public static void SelectMode() {
		Application.LoadLevel("SelectMode");
	}

	public static void SelectLevel() {
		Application.LoadLevel("SelectLevel");
	}
	
	public static void SelectPlayer() {
		Application.LoadLevel("SelectPlayer");
	}

	public static void Credits() {
		Debug.Log("credits");
	}
	
	public static void Summary() {
		Application.LoadLevel("Summary");
	}
	
	public static void Award() {
		Application.LoadLevel("Award");
	}
	
	public static void StartGame() {
		GameManager.Instance.Level = (GameManager.eLevels)1;
		Application.LoadLevel(GameManager.Instance.Level.ToString());
	}

	public static void NextLevel() {
		Debug.Log ("next " + GameManager.Instance.Level.ToString ());
		Application.LoadLevel (GameManager.Instance.Level.ToString ());
	}
	
	public static void StartLevel(GameManager.eLevels level) {
		GameManager.Instance.Level = level;
		Application.LoadLevel (((GameManager.eLevels)level.GetHashCode ()).ToString ());
	}
}
