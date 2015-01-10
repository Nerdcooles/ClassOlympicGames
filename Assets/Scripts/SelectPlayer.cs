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

//	public void BlueTap()
//	{
//			pos++;
//			blue.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/blue_p0" + pos);
//			blue.SetActive(true);
//			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.blue);
//			CheckNumber();
//	}
//	
//	public void RedTap()
//	{
//			pos++;
//			red.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/red_p0" + pos);
//			red.SetActive(true);
//			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.red);
//			CheckNumber();
//	}
//	
//	public void GreenTap()
//	{
//			pos++;
//			green.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/green_p0" + pos);
//			green.SetActive(true);
//			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.green);
//			CheckNumber();
//	}
//	
//	public void YellowTap()
//	{
//			pos++;
//			yellow.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/yellow_p0" + pos);
//			yellow.SetActive(true);
//			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.yellow);
//			CheckNumber();
//	}
	
	private void CheckNumber() {
		if(pos == GameManager.Instance.getNumPlayer())
			MenuManager.startGame();
	}
	
	public void Back ()
	{
		MenuManager.selectNumber();		
	}
}
