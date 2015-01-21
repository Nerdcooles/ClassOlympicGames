using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwardManager : MonoBehaviour {
	
	public GameObject[] pod;
	public GameObject[] button;
	private const int X_2P = 250;

	void Start() {

		/**************************************  TEST CASES  **************************************
		GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
		GameManager.Instance.createPlayers (2);
		for(int i=0; i<2; i++)
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
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		
		//P4
		//GOLD
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);

		//**************************************  TEST CASES  **************************************/

		try{
			List<GameManager.ePlayers> winners = GameManager.Instance.getWinners (false);
			
			for (int i=0; i<winners.Count; i++) {
				GameManager.ePlayers player = winners[i];
				if(i!=3) {
					//PODIUM
					pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_winner");
				}else{
					//OUT
					pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_loser");
				}
				pod[i].SetActive(true);
			}
		}catch(DrawException ex){

		}

		int num_players = GameManager.Instance.getNumPlayer ();
		float y = button[0].GetComponent<RectTransform>().position.y;
		float width = (float)Screen.width/2f;
		switch(num_players) {
		case 2: 
			button[0].GetComponent<RectTransform>().position = new Vector3(-X_2P, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(X_2P, y, 0f);
			button[2].SetActive(false);
			button[3].SetActive(false);
			break;
		case 3: 
			button[1].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); 
			button[2].GetComponent<RectTransform>().position = button[3].GetComponent<RectTransform>().position;
			button[3].SetActive(false);
			break;
		}
	}

	void Update() {
		if (Input.anyKeyDown)
			Skip ();
	}

	public void Skip() {
		MenuManager.NewGame ();
	}
}
