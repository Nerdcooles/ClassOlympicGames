using UnityEngine;
using System.Collections.Generic;

public class Summary : MonoBehaviour {

	public GameObject[] pod;

	GameObject[] medalPrefab = new GameObject[3];
	float x;
	float[] y = new float[4];
	float z;

	void Start () {

		/**************************************  TEST CASES  **************************************
		GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
		GameManager.Instance.createPlayers (4);
		for(int i=0; i<4; i++)
			GameManager.Instance.setColor((GameManager.ePlayers)i, (GameManager.eColors)i);
		//P1
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		
		//P2
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//BRONZE

		//P3
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);

		//P4
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);

		//**************************************  TEST CASES  **************************************/

		for(int i=0;i<3;i++){
			medalPrefab[i] = Resources.Load<GameObject> ("Prefabs/Medal_" + i);
			medalPrefab[i].GetComponent<MedalMovement>().enabled = false;

		}
		x = pod [0].transform.position.x;
		z = pod [0].transform.position.z;
		for (int i=0; i<4; i++)
						y[i] = pod [i].transform.position.y;

		setPositions ();
	}

	void setPositions() {
		List<GameManager.ePlayers> winners = GameManager.Instance.getWinners(true);

		int pos = 0;
		foreach(GameManager.ePlayers p in winners) {
			pod[p.GetHashCode()].transform.position = new Vector3(x,y[pos],z);
			pos++;
		}

		for (int i=0; i<winners.Count; i++) {

			pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor ((GameManager.ePlayers)i) + "_podium_winner");

			for(int material = 0; material<3; material++) {
				int tot = GameManager.Instance.getMedal((GameManager.ePlayers)i,(GameManager.eMedals)material);
				for(int num=0; num < tot; num++) {
					Instantiate(medalPrefab[material], pod[i].transform.position + new Vector3((140+material*200) + (num/2)*40,-20 + 40*(num%2),0f), transform.rotation);
				}
			}
			pod[i].SetActive(true);
		}
	}

	void Update() {
		if (Input.anyKeyDown)
						Skip ();
		}
	public void Skip() {
		MenuManager.NextLevel ();
	}
}
