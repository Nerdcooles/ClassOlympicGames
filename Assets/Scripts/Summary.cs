using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Summary : MonoBehaviour {

	Text[] medals = new Text[4];

	void Start () {
		for (int i=0; i<4; i++) {
						medals[i] = GameObject.Find ("P" + (i+1)).GetComponent<Text>();
						medals [i].text = "";
				}

		
		foreach (GameManager.ePlayers p in GameManager.Instance.getPlayers()) {
			medals[p.GetHashCode()].text = p.ToString () + " " + GameManager.Instance.getColor (p) 
			           + " GOLD " + GameManager.Instance.getMedal (p, GameManager.eMedals.Gold)
			           + " SILVER " + GameManager.Instance.getMedal (p, GameManager.eMedals.Silver)
			           + " BRONZE " + GameManager.Instance.getMedal (p, GameManager.eMedals.Bronze);
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
