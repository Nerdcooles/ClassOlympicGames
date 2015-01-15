using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectLevel : MonoBehaviour {

	public void startBus()
	{
		MenuManager.StartLevel(GameManager.eLevels.Delaybus);
	}
	
	public void startBucket()
	{
		MenuManager.StartLevel(GameManager.eLevels.Bucketball);
	}
	
	public void startArchery()
	{
		MenuManager.StartLevel(GameManager.eLevels.Arteachery);
	}
	
	public void startSkip()
	{
		MenuManager.StartLevel(GameManager.eLevels.SkipTheTest);
	}

	public void Back() {
		MenuManager.SelectPlayer();
	}
}
