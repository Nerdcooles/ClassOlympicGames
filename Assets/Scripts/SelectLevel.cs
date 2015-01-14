using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectLevel : MonoBehaviour {

	public void startBus()
	{
		MenuManager.StartLevel(GameManager.eLevels.Bus);
	}
	
	public void startBucket()
	{
		MenuManager.StartLevel(GameManager.eLevels.Bucket);
	}
	
	public void startArchery()
	{
		MenuManager.StartLevel(GameManager.eLevels.Archery);
	}

	public void Back() {
		MenuManager.SelectPlayer();
	}
}
