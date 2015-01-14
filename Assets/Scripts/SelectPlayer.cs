using UnityEngine;
using UnityEngine.UI;
using System;
using TouchScript.Gestures;


public class SelectPlayer : MonoBehaviour {

	public GameObject[] toggle;
	public GameObject[] checkmark;
	
	private int pos;
	
	void Start() {
		pos = 0;
	}

	public void Select (int player) {
		toggle[player].GetComponent<Toggle> ().interactable = false;

		switch (player) {
		case 0: checkmark[player].GetComponent<Image>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/blue_p0" + (pos+1)); GameManager.Instance.setColor((GameManager.ePlayers)(pos), GameManager.eColors.blue);break;
		case 1: checkmark[player].GetComponent<Image>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/green_p0" + (pos+1)); GameManager.Instance.setColor((GameManager.ePlayers)(pos), GameManager.eColors.green);break;
		case 2: checkmark[player].GetComponent<Image>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/red_p0" + (pos+1)); GameManager.Instance.setColor((GameManager.ePlayers)(pos), GameManager.eColors.red);break;
		case 3: checkmark[player].GetComponent<Image>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/yellow_p0" + (pos+1)); GameManager.Instance.setColor((GameManager.ePlayers)(pos), GameManager.eColors.yellow);break;
		}
		pos++;
		CheckNumber();
	}


	private void CheckNumber() {
		if (pos == GameManager.Instance.getNumPlayer ()) {
			switch(GameManager.Instance.getGameMode()){
				case GameManager.eGameMode.CLASSIC: MenuManager.StartGame (); break;
				case GameManager.eGameMode.TRAINING: MenuManager.SelectLevel (); break;
			}
		}
	}
	
	public void Back ()
	{
		MenuManager.SelectNumber();		
	}
}
