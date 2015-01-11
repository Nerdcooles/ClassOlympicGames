using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectLevel : MonoBehaviour {

	public void startBus()
	{
		MenuManager.startLevel(GameManager.eLevels.Bus);
	}
	
	public void startBucket()
	{
		MenuManager.startLevel(GameManager.eLevels.Bucket);
	}
	
	public void startArchery()
	{
		MenuManager.startLevel(GameManager.eLevels.Archery);
	}

	public void Back() {
		MenuManager.selectPlayer();
	}
}
