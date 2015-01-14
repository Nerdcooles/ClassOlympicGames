﻿using UnityEngine;
using UnityEngine.UI;
using System;
using TouchScript.Gestures;


public class SelectNumber : MonoBehaviour {

	GameObject btn_1p;
	
	void Awake() {
		if(GameManager.Instance.getGameMode() == GameManager.eGameMode.CLASSIC) {
			btn_1p = GameObject.Find ("btn_1p");
			btn_1p.GetComponent<Button>().interactable = false;
		}
	}

	public void StartWithPlayers(int num_players)
	{
		GameManager.Instance.createPlayers(num_players);
		MenuManager.SelectPlayer();
	}
	
	public void Back ()
	{
		MenuManager.SelectMode();
	}
}
