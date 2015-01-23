using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIManager : MonoBehaviour {

	private GameObject[] player = new GameObject[4];
	private GameObject[] button = new GameObject[4];

	private const int X_2P = 250;

	private int num_players;

	private Text[] scoreP;

	private float sceneWidth;
	private float sceneHeight;

	void Awake () {
		sceneWidth = -Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).x;
		sceneHeight = -Camera.main.ScreenToWorldPoint(new Vector3(0f,0f,0f)).y;

		try {
						initPlayers ();
				} catch (System.Exception ex) {
			Debug.Log("Award level");
		}
		initButtons();
		try {
						initScoring ();
		} catch (System.Exception ex) {
					Debug.Log("No scoring view");
		}

	} 

	private void initPlayers() {
		player = new GameObject[4];
		for (int i=0; i<4; i++) {
			player [i] = GameObject.Find ("p0" + (i + 1));
		}
		num_players = GameManager.Instance.getNumPlayer ();
		switch (num_players) {
			case 1:
				player [0].transform.position = player[2].transform.position;
				player [0].transform.rotation = player[2].transform.rotation;
			player[1].SetActive(false);
			player[2].SetActive(false);
			player[3].SetActive(false);
				break;
			case 2:
				player [0].transform.position = player [1].transform.position;
				player [0].transform.rotation = player [1].transform.rotation;
				player [1].transform.position = player [2].transform.position;
				player [1].transform.rotation = player [2].transform.rotation;
			
			player[2].SetActive(false);
			player[3].SetActive(false);
				break;
			case 3:
				player [0].transform.position = player [1].transform.position;
				player [0].transform.rotation = player [1].transform.rotation;
				player [1].transform.position = player [2].transform.position;
				player [1].transform.rotation = player [2].transform.rotation;
				player [2].transform.position = player [3].transform.position;
			player [2].transform.rotation = player [3].transform.rotation;

			player[3].SetActive(false);
				break;
		}
		}
	private void initButtons() {

		for (int i=0; i<4; i++)
			button [i] = GameObject.Find ("btn_p0" + (i + 1));

		float y = button[0].GetComponent<RectTransform>().position.y;
		float width = (float)Screen.width/2f;
		switch(num_players) {
		case 1: 
			button[0].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); break;
		case 2: 
			button[0].GetComponent<RectTransform>().position = new Vector3(-sceneWidth/2f, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(sceneWidth/2f, y, 0f);
			break;
		case 3: 
			button[1].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); 
			button[2].GetComponent<RectTransform>().position = button[3].GetComponent<RectTransform>().position;
			break;
		case 4: 
			button[0].GetComponent<RectTransform>().position = new Vector3(-sceneWidth*3f/4f, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(-sceneWidth/4f, y, 0f);
			button[2].GetComponent<RectTransform>().position = new Vector3(sceneWidth/4f, y, 0f);  
			button[3].GetComponent<RectTransform>().position = new Vector3(sceneWidth*3f/4f, y, 0f);

			break;
		}
	}
	
	private void initScoring() {
		scoreP = new Text[4];
		for(int i=0; i<4; i++) {
			scoreP[i] = GameObject.Find("ScoreP"+(i+1)).GetComponent<Text>();
			if(i<num_players) {
				scoreP[i].text = "0";
				scoreP[i].color = GameManager.Instance.getSysColor((GameManager.ePlayers)i);
			}else {
				scoreP[i].gameObject.SetActive(false);
			}
		}

		float mid = (scoreP [1].GetComponent<RectTransform> ().position.x + scoreP [2].GetComponent<RectTransform> ().position.x) / 2f;
		float y = scoreP [1].GetComponent<RectTransform> ().position.y;
		float z = scoreP [1].GetComponent<RectTransform> ().position.z;
		switch (num_players) {
		case 1: scoreP[0].GetComponent<RectTransform>().position = new Vector3(mid, y, z);
			
			break;
		case 2: scoreP[0].GetComponent<RectTransform>().position = scoreP[1].GetComponent<RectTransform>().position;
			scoreP[1].GetComponent<RectTransform>().position = scoreP[2].GetComponent<RectTransform>().position;
			
			break;
		case 3: //scoreP0 nothing
			scoreP[1].GetComponent<RectTransform>().position = new Vector3(mid, y, z);
			scoreP[2].GetComponent<RectTransform>().position = scoreP[3].GetComponent<RectTransform>().position;
			break;
		}
	}

	public void score(GameManager.ePlayers player, int pts) {
		scoreP [player.GetHashCode ()].text = pts.ToString();

		}

	public float SceneWidth {
		get {
			return sceneWidth;
		}
	}
	
	public float SceneHeight {
		get {
			return sceneHeight;
		}
	}

	public GameObject getButton(GameManager.ePlayers player) {
		return button[player.GetHashCode()];
	}
}
