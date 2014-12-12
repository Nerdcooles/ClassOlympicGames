using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUI : MonoBehaviour {

	public enum ePanel {Scores, Podium, Medals}
	
	public GameObject panel_Scores;
	public Text[] txt_score;
	public GameObject panel_Podium;
	public Text[] txt_medal;
	public GameObject panel_Medals;
	public Text[] p1_medals;
	public Text[] p2_medals; 
	public Text[] p3_medals; 
	public Text[] p4_medals; 
	

	void Start() {
		panel_Scores.SetActive(false);
		panel_Podium.SetActive(false);
		panel_Medals.SetActive(false);
	}

	public void show(ePanel panel) {
		switch(panel) {
			case ePanel.Scores: panel_Scores.SetActive(true); break;
			case ePanel.Podium: panel_Podium.SetActive(true); break;
			case ePanel.Medals: loadMedals(); panel_Medals.SetActive(true); break;
		}
	}
	
	public void hide(ePanel panel) {
		switch(panel) {
		case ePanel.Scores: panel_Scores.SetActive(false); break;
		case ePanel.Podium: panel_Podium.SetActive(false); break;
		case ePanel.Medals: panel_Medals.SetActive(false); break;
		}
	}

	public void score(GameManager.ePlayers player, string score) {
		txt_score[player.GetHashCode()].text = score;
	}

	public void medal(GameManager.ePlayers player, GameManager.eMedals medal) {
		txt_medal[medal.GetHashCode()].text = player.ToString();
	}

	private void loadMedals() {
		for(int i=0; i<3; i++){
			p1_medals[i].text = GameManager.Instance.getMedal(GameManager.ePlayers.p01, (GameManager.eMedals)i).ToString();
			p2_medals[i].text = GameManager.Instance.getMedal(GameManager.ePlayers.p02, (GameManager.eMedals)i).ToString();
			p3_medals[i].text = GameManager.Instance.getMedal(GameManager.ePlayers.p03, (GameManager.eMedals)i).ToString();
			p4_medals[i].text = GameManager.Instance.getMedal(GameManager.ePlayers.p04, (GameManager.eMedals)i).ToString();
		}
	}
}
