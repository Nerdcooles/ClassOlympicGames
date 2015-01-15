using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject hud_tablet, hud_phone_4p;
	private GameObject[] player;
	private const int X_2P = 250;
	bool isPhone4p = false;
	private int num_players;
	private Text[] scoreP;

	void Awake () {
		#if UNITY_IPHONE || UNITY_ANDROID
			#if UNITY_EDITOR
			hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = true;
			hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = true;
			#else	
			hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = false;
			hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = false;
		#endif
		float res = (float)Screen.width/Screen.height;
		if(res > 1.5f && GameManager.Instance.getNumPlayer() == 4) {
			isPhone4p = true;
			hud_tablet.SetActive(false);
			hud_phone_4p.SetActive(true);
		}else{
			hud_tablet.SetActive(true);
			hud_phone_4p.SetActive(false);
		}
		#else
		hud_tablet.SetActive(true);
		hud_phone_4p.SetActive(false);
		#endif

		#if UNITY_EDITOR
				hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = true;
				hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = true;
		#endif

		initPlayers ();
		initButtons();
		try {
			initScoring();
		}catch(System.NullReferenceException e) {
			Debug.Log("No scoring on screen");
		}
	} 

	private void initPlayers() {
		player = new GameObject[4];
		for (int i=0; i<4; i++)
			player [i] = GameObject.Find ("Player" + (i + 1));
		num_players = GameManager.Instance.getNumPlayer ();
		switch (num_players) {
		case 1:
			player [0].transform.position = player[2].transform.position;
			player [0].transform.rotation = player[2].transform.rotation;
			
			break;
		case 2:
			player [0].transform.position = player [1].transform.position;
			player [0].transform.rotation = player [1].transform.rotation;
			player [1].transform.position = player [2].transform.position;
			player [1].transform.rotation = player [2].transform.rotation;
			
			break;
		case 3:
			player [0].transform.position = player [1].transform.position;
			player [0].transform.rotation = player [1].transform.rotation;
			player [1].transform.position = player [2].transform.position;
			player [1].transform.rotation = player [2].transform.rotation;
			player [2].transform.position = player [3].transform.position;
			player [2].transform.rotation = player [3].transform.rotation;
			break;
		}
		}
	private void initButtons() {
		GameObject[] button  = hud_tablet.GetComponent<HudKeyboardAdapter>().button;
		float y = button[0].GetComponent<RectTransform>().position.y;
		float width = (float)Screen.width/2f;
		switch(num_players) {
		case 1: button[0].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); break;
		case 2: button[0].GetComponent<RectTransform>().position = new Vector3(-X_2P, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(X_2P, y, 0f);break;
		case 3: 
			button[1].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); 
			button[2].GetComponent<RectTransform>().position = button[3].GetComponent<RectTransform>().position;break;
		}
	}
	
	private void initScoring() {
		scoreP = new Text[4];
		for(int i=0; i<4; i++) {
			scoreP[i] = GameObject.Find("ScoreP"+(i+1)).GetComponent<Text>();
			if(i<num_players) {
				scoreP[i].text = "0";
				scoreP[i].color = GameManager.Instance.getSysColor((GameManager.ePlayers)i);
			}else
				scoreP[i].gameObject.SetActive(false);
			
		}
		
		switch (num_players) {
		case 1: scoreP[0].GetComponent<RectTransform>().position = new Vector3(-164, 251, 0);
			
			break;
		case 2: scoreP[0].GetComponent<RectTransform>().position = scoreP[1].GetComponent<RectTransform>().position;
			scoreP[1].GetComponent<RectTransform>().position = scoreP[2].GetComponent<RectTransform>().position;
			
			break;
		case 3: //scoreP0 nothing
			scoreP[1].GetComponent<RectTransform>().position = new Vector3(-164, 251, 0);
			scoreP[2].GetComponent<RectTransform>().position = scoreP[3].GetComponent<RectTransform>().position;
			break;
		}
	}

	public void score(GameManager.ePlayers player, int pts) {
		scoreP [player.GetHashCode ()].text = pts.ToString();

		}

	public GameObject getButton(GameManager.ePlayers player) {
		GameObject[] button;
		if (isPhone4p) {
			button  = hud_phone_4p.GetComponent<HudKeyboardAdapter>().button;
				} else {
			button  = hud_tablet.GetComponent<HudKeyboardAdapter>().button;
				}
		return button[player.GetHashCode()];
	}
}
