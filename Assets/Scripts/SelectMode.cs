using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectMode : MonoBehaviour {

	
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

	public void StartScores ()
	{
		Debug.Log("Best Scores");
	}

	public void Back ()
	{
		MenuManager.StartHome();
	}
	
}
