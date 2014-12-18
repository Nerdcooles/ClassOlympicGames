using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUI : MonoBehaviour {

	public enum ePanel {Countdown, Scores, Podium, Medals}
	
	public Text[] p1_name;
	public Text[] p2_name;
	public Text[] p3_name;
	public Text[] p4_name;

	public GameObject panel_Scores;
	public Text[] txt_score;
	public GameObject panel_Podium;
	public Text[] txt_medal;
	public GameObject panel_Medals;
	public Text[] p1_medals;
	public Text[] p2_medals; 
	public Text[] p3_medals; 
	public Text[] p4_medals;
	public GameObject panel_Countdown;
	public Text three;
	public Text two;
	public Text one;
	public GameObject panel_Scoreboard;
	public Text[] live_score;
	
	void Start() {
		disablePlayers();
		panel_Scores.SetActive(false);
		panel_Podium.SetActive(false);
		panel_Medals.SetActive(false);
		panel_Countdown.SetActive(true);
		setNames();
	}

	private void disablePlayers() {
		int num_players = GameManager.Instance.getNumPlayer();
		float padding = 0;
		switch(num_players) {
			case 1: if(padding==0) padding = 2.5f; live_score[1].gameObject.SetActive(false);foreach(Text txt in p2_name) txt.gameObject.SetActive(false); goto case 2;
			case 2: if(padding==0) padding = 1.5f; live_score[2].gameObject.SetActive(false);foreach(Text txt in p3_name) txt.gameObject.SetActive(false); goto case 3;
			case 3: if(padding==0) padding = 0.7f; live_score[3].gameObject.SetActive(false);foreach(Text txt in p4_name) txt.gameObject.SetActive(false); break;
		}
		panel_Scoreboard.transform.position += transform.right * padding;
	}

	private void setNames() {
		foreach(Text name in p1_name)
			name.text = GameManager.Instance.getName(GameManager.ePlayers.p01);
		foreach(Text name in p2_name)
			name.text = GameManager.Instance.getName(GameManager.ePlayers.p02);
		foreach(Text name in p3_name)
			name.text = GameManager.Instance.getName(GameManager.ePlayers.p03);
		foreach(Text name in p4_name)
			name.text = GameManager.Instance.getName(GameManager.ePlayers.p04);
	}

	public void show(ePanel panel) {
		switch(panel) {
			case ePanel.Scores: panel_Scores.SetActive(true); break;
			case ePanel.Podium: panel_Podium.SetActive(true); break;
			case ePanel.Medals: loadMedals(); panel_Medals.SetActive(true); break;
			case ePanel.Countdown: panel_Countdown.SetActive(true); break;
		}
	}
	
	public void hide(ePanel panel) {
		switch(panel) {
		case ePanel.Scores: panel_Scores.SetActive(false); break;
		case ePanel.Podium: panel_Podium.SetActive(false); break;
		case ePanel.Medals: panel_Medals.SetActive(false); break;
		case ePanel.Countdown: panel_Countdown.SetActive(false); break;
		}
	}

	public void countdown(int sec) {
		switch(sec) {
		case 3: three.gameObject.SetActive(true); two.gameObject.SetActive(false); one.gameObject.SetActive(false); break;
		case 2: three.gameObject.SetActive(false); two.gameObject.SetActive(true); one.gameObject.SetActive(false); break;
		case 1: three.gameObject.SetActive(false); two.gameObject.SetActive(false); one.gameObject.SetActive(true); break;
		}
	}

	public void score(GameManager.ePlayers player, string score) {
		txt_score[player.GetHashCode()].text = score;
		live_score[player.GetHashCode()].text = score;
	}

	public void medal(GameManager.ePlayers player, GameManager.eMedals medal) {
		txt_medal[medal.GetHashCode()].text = GameManager.Instance.getName(player);
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
