using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwardManager : MonoBehaviour {


	void Start() {

		/**************************************  TEST CASES  **************************************
		GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
		GameManager.Instance.createPlayers (4);
		for(int i=0; i<4; i++)
			GameManager.Instance.setColor((GameManager.ePlayers)i, (GameManager.eColors)i);
		//P1
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		
		//P2
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		
		//P3
		//GOLD
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		
		//P4
		//GOLD
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		
		List<GameManager.ePlayers> winners = GameManager.Instance.getWinners ();
		Debug.Log ("Number of ps " + winners.Count);
		for (int i=0; i<winners.Count; i++)
						Debug.Log ("Pos" + i + " " + winners [i]);
		**************************************  TEST CASES  **************************************/


	}

	void Update() {
		if (Input.anyKeyDown)
			Skip ();
	}

	public void Skip() {
		MenuManager.NewGame ();
	}
}
