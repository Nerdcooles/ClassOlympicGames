using UnityEngine;
using System.Collections.Generic;

public class Summary : MonoBehaviour {

	public GameObject[] pod;

	public GameObject[] medalPrefab;
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
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p01, GameManager.eMedals.Bronze);
		
		//P2
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p02, GameManager.eMedals.Bronze);
		
		//P3
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p03, GameManager.eMedals.Bronze);
		
		//P4
		//GOLD
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Gold);
		//SILVER
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Silver);
		//BRONZE
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);
		GameManager.Instance.addMedal (GameManager.ePlayers.p04, GameManager.eMedals.Bronze);

		//**************************************  TEST CASES  **************************************/
		x = pod [0].transform.position.x;
		z = pod [0].transform.position.z;
		for (int i=0; i<4; i++)
						y[i] = pod [i].transform.position.y;

		setPositions ();
	}

	void setPositions() {
		int num_players = GameManager.Instance.getNumPlayer ();

		switch (num_players) {
		case 2: pod[0].transform.position = new Vector3(x,y[1],z);pod[1].transform.position = new Vector3(x,y[2],z); pod[2].SetActive(false);pod[3].SetActive(false);break;
		case 3: pod[0].transform.position = new Vector3(x,(y[0]+y[1])/2f,z);pod[1].transform.position = new Vector3(x,(y[1]+y[2])/2f,z);pod[2].transform.position = new Vector3(x,(y[2]+y[3])/2f,z);pod[3].SetActive(false);break;
		}

		for (int i=0; i<num_players; i++) {

			pod[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor ((GameManager.ePlayers)i) + "_podium_winner");

			for(int medal = 0; medal<3; medal++) {
				for(int tot = GameManager.Instance.getMedal((GameManager.ePlayers)i,(GameManager.eMedals)medal); tot > 0; tot--) {
					Instantiate(medalPrefab[medal], pod[i].transform.position + new Vector3(((1+4f*medal) * 50f) + (tot * 30f),0f,0f), transform.rotation);
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
