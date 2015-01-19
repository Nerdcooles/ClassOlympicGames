using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectMode : GenericMenu {

	
	public void StartClassic ()
	{
		GameManager.Instance.startMode(GameManager.eGameMode.CLASSIC);
		MenuManager.SelectNumber();
	}
	
	public void StartTraining ()
	{
		GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
		MenuManager.SelectNumber();
	}
}	
