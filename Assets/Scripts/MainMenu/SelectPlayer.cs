using UnityEngine;using UnityEngine.UI;using System;using TouchScript.Gestures;/** * Select player page behavior */public class SelectPlayer : GenericMenu{	GameObject[] toggle = new GameObject[4];	GameObject[] checkmark = new GameObject[4];	private int pos = 0;	void Start ()	{		for (int i=0; i<4; i++) {			toggle [i] = GameObject.Find ("Toggle_p" + (i + 1));			checkmark [i] = GameObject.Find ("Checkmark_p" + (i + 1));		}	}	/**	 * Called when user press on a character	 * 	 * \param player Player's number	 */	public void Select (int player)	{		toggle [player].GetComponent<Toggle> ().interactable = false;		switch (player) {		case 0: 			checkmark [player].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Buttons/blue_p0" + (pos + 1));			GameManager.Instance.SetColor ((GameManager.ePlayers)(pos), GameManager.eColors.blue);			break;		case 1:			checkmark [player].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Buttons/green_p0" + (pos + 1));			GameManager.Instance.SetColor ((GameManager.ePlayers)(pos), GameManager.eColors.green);			break;		case 2: 			checkmark [player].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Buttons/red_p0" + (pos + 1));			GameManager.Instance.SetColor ((GameManager.ePlayers)(pos), GameManager.eColors.red);							break;		case 3: 			checkmark [player].GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Buttons/yellow_p0" + (pos + 1));			GameManager.Instance.SetColor ((GameManager.ePlayers)(pos), GameManager.eColors.yellow);			break;		}		//sprite button 3 is larger		if (pos == 2)			checkmark [player].GetComponent<RectTransform> ().sizeDelta += new Vector2 (26f, 0);		pos++;		CheckNumber ();	}	/**	 * Start game when every users select a character	 */	private void CheckNumber ()	{		if (pos == GameManager.Instance.GetNumPlayer ()) {			switch (GameManager.Instance.GetGameMode ()) {			case GameManager.eGameMode.CLASSIC:				MenuManager.StartGame ();				break;			case GameManager.eGameMode.TRAINING:				MenuManager.SelectLevel ();				break;			}		}	}}