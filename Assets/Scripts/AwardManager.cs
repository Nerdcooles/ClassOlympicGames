using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwardManager : MonoBehaviour {
	
	private GameObject podiumPrefab;

	private const float X_GOLD = -10f;
	private const float Y_GOLD = 90f;
	
	private const float X_SILVER = 170f;
	private const float Y_SILVER = 70f;
	
	private const float X_BRONZE = -170f;
	private const float Y_BRONZE = 40f;
	
	private const float X_NC = -390f;
	private const float Y_NC = -7f;
	
	private bool canSkip = false;
	private int secToSkip = 2;

	void Start() {
		podiumPrefab = Resources.Load<GameObject> ("Prefabs/Podium") as GameObject;

		//**************************************  TEST CASES  **************************************
		GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
		GameManager.Instance.createPlayers (4);
		for(int i=0; i<4; i++)
			GameManager.Instance.setColor((GameManager.ePlayers)i, (GameManager.eColors)i);
		//P1
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		
		//P2
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//SILVER
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		
		//P3
		//GOLD
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		
		//P4
		//GOLD
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		//BRONZE
		//GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);

		//**************************************  TEST CASES  **************************************/

		try{
			List<GameManager.ePlayers> winners = GameManager.Instance.getWinners (false);
			
			for (int i=0; i<winners.Count; i++) {
				GameManager.ePlayers player = winners[i];
				GameObject podInstance = Instantiate(podiumPrefab) as GameObject;
				switch(i) {
				case 0: podInstance.transform.position = new Vector2(X_GOLD, Y_GOLD); break;
				case 1: podInstance.transform.position = new Vector2(X_SILVER, Y_SILVER); break;
				case 2: podInstance.transform.position = new Vector2(X_BRONZE, Y_BRONZE); break;
				case 3: podInstance.transform.position = new Vector2(X_NC, Y_NC); break;
				}
				if(i!=3) {
					//PODIUM
					podInstance.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_winner");
				}else{
					//OUT
					podInstance.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_loser");
				}
			}
		}catch(DrawException ex){
			List<GameManager.ePlayers> winners = GameManager.Instance.getWinners (true);

			int position = 0;
			for (int i=0; i<winners.Count; i++) {
				int player_pts = GameManager.Instance.GetPoints(winners[i]);
				int next_player_pts = -1;
				try{
				next_player_pts = GameManager.Instance.GetPoints(winners[i+1]);
				}catch{
					next_player_pts = -1;
				}
				if( player_pts == next_player_pts) {
					next_player_pts = -1;
					try{
						next_player_pts = GameManager.Instance.GetPoints(winners[i+2]);
					}catch{
						next_player_pts = -1;
					}
					if(player_pts == next_player_pts) {
						next_player_pts = -1;
						try{
							next_player_pts = GameManager.Instance.GetPoints(winners[i+3]);
						}catch{
							next_player_pts = -1;
						}
						if(player_pts == next_player_pts) {
							//FOUR DRAW
							GameObject podInstance1 = Instantiate(podiumPrefab) as GameObject;
							GameObject podInstance2 = Instantiate(podiumPrefab) as GameObject;
							GameObject podInstance3 = Instantiate(podiumPrefab) as GameObject;
							GameObject podInstance4 = Instantiate(podiumPrefab) as GameObject;

							switch(position) {
							case 0: 
								podInstance1.transform.position = new Vector3(X_GOLD-20, Y_GOLD-20, -1); 
								podInstance4.transform.position = new Vector3(X_GOLD+60, Y_GOLD-20, -1); 

								podInstance2.transform.position = new Vector3(X_GOLD-60, Y_GOLD+20,0);
								podInstance3.transform.position = new Vector3(X_GOLD+20, Y_GOLD+20,0);
								break;
							case 1: 
								podInstance1.transform.position = new Vector3(X_SILVER-20, Y_SILVER-20, -1);
								podInstance4.transform.position = new Vector3(X_SILVER+60, Y_SILVER-20, -1);

								podInstance2.transform.position = new Vector3(X_SILVER-60, Y_SILVER+20,0);
								podInstance3.transform.position = new Vector3(X_SILVER+20, Y_SILVER+20,0); 
								break;
							case 2: 
								podInstance1.transform.position = new Vector3(X_BRONZE-20, Y_BRONZE-20, -1);
								podInstance4.transform.position = new Vector3(X_BRONZE+60, Y_BRONZE-20, -1);

								podInstance2.transform.position = new Vector3(X_BRONZE-60, Y_BRONZE+20); 
								podInstance3.transform.position = new Vector3(X_BRONZE+20, Y_BRONZE+20); 
								break;
							case 3: 
								podInstance1.transform.position = new Vector3(X_NC-20, Y_NC-20, -1);
								podInstance4.transform.position = new Vector3(X_NC+60, Y_NC-20, -1);

								podInstance2.transform.position = new Vector3(X_NC-60, Y_NC+20,0); 
								podInstance3.transform.position = new Vector3(X_NC+20, Y_NC+20,0); 
								break;
							}
							
							podInstance1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i]) + "_podium_winner");
							podInstance2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+1]) + "_podium_winner");
							podInstance3.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+2]) + "_podium_winner");
							podInstance4.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+3]) + "_podium_winner");

							i += 3;

						}else{

							//THREE DRAW
							GameObject podInstance1 = Instantiate(podiumPrefab) as GameObject;
							GameObject podInstance2 = Instantiate(podiumPrefab) as GameObject;
							GameObject podInstance3 = Instantiate(podiumPrefab) as GameObject;
							
							switch(position) {
							case 0: 
								podInstance1.transform.position = new Vector3(X_GOLD, Y_GOLD-20, -1); 
								podInstance2.transform.position = new Vector3(X_GOLD-60, Y_GOLD+20,0);
								podInstance3.transform.position = new Vector3(X_GOLD+20, Y_GOLD+20,0);
								break;
							case 1: 
								podInstance1.transform.position = new Vector3(X_SILVER, Y_SILVER-20, -1);
								podInstance2.transform.position = new Vector3(X_SILVER-60, Y_SILVER+20,0);
								podInstance3.transform.position = new Vector3(X_SILVER+20, Y_SILVER+20,0); 
								break;
							case 2: 
								podInstance1.transform.position = new Vector3(X_BRONZE, Y_BRONZE-20, -1);
								podInstance2.transform.position = new Vector3(X_BRONZE-60, Y_BRONZE+20,0); 
								podInstance3.transform.position = new Vector3(X_BRONZE+20, Y_BRONZE+20,0); 
								break;
							case 3: 
								podInstance1.transform.position = new Vector3(X_NC, Y_NC-20, -1);
								podInstance2.transform.position = new Vector3(X_NC-60, Y_NC+20,0); 
								podInstance3.transform.position = new Vector3(X_NC+20, Y_NC+20,0); 
								break;
							}
							
							podInstance1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i]) + "_podium_winner");
							podInstance2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+1]) + "_podium_winner");
							podInstance3.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+2]) + "_podium_winner");

							i += 2;
						}
					}else{
						// TWO DRAW

						GameObject podInstance1 = Instantiate(podiumPrefab) as GameObject;
						GameObject podInstance2 = Instantiate(podiumPrefab) as GameObject;

						switch(position) {
						case 0: 
							podInstance1.transform.position = new Vector2(X_GOLD-40, Y_GOLD); 
							podInstance2.transform.position = new Vector2(X_GOLD+40, Y_GOLD);
							break;
						case 1: 
							podInstance1.transform.position = new Vector2(X_SILVER-40, Y_SILVER);
							podInstance2.transform.position = new Vector2(X_SILVER+40, Y_SILVER); 
							break;
						case 2: 
							podInstance1.transform.position = new Vector2(X_BRONZE-40, Y_BRONZE);
							podInstance2.transform.position = new Vector2(X_BRONZE+40, Y_BRONZE); 
							break;
						case 3: 
							podInstance1.transform.position = new Vector2(X_NC-40, Y_NC);
							podInstance2.transform.position = new Vector2(X_NC+40, Y_NC); 
							break;
						}

						podInstance1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i]) + "_podium_winner");
						podInstance2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i+1]) + "_podium_winner");


						i += 1;

					}
				}else{
					GameManager.ePlayers player = winners[i];
					GameObject podInstance = Instantiate(podiumPrefab) as GameObject;
					switch(position) {
					case 0: podInstance.transform.position = new Vector2(X_GOLD, Y_GOLD); break;
					case 1: podInstance.transform.position = new Vector2(X_SILVER, Y_SILVER); break;
					case 2: podInstance.transform.position = new Vector2(X_BRONZE, Y_BRONZE); break;
					case 3: podInstance.transform.position = new Vector2(X_NC, Y_NC); break;
					}
					podInstance.GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (winners[i]) + "_podium_winner");
				}

				position++;

//				if(i!=3) {
//					//PODIUM
//					pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_winner");
//				}else{
//					//OUT
//					pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_loser");
//				}
//				pod[i].SetActive(true);
		}

	}
	}

	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip)
			MenuManager.NewGame ();
	}
	
	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			canSkip=true;
			CancelInvoke("WaitToSkip");
			
		}
	}
}
