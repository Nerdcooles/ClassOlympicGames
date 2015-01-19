using UnityEngine;
using TouchScript.Gestures;
using System;


public class Home : GenericMenu {

	public void StartGame()
	{
		MenuManager.SelectMode();
	}

	public void Credits()
	{
		MenuManager.Credits();
	}

}
