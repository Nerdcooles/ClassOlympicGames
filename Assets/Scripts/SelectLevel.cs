﻿using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectLevel : GenericMenu {

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
	
	public void startNerd()
	{
		MenuManager.StartLevel(GameManager.eLevels.Nerdthrow);
	}
	
	public void startLongboard()
	{
		MenuManager.StartLevel(GameManager.eLevels.LongboardJump);
	}
}
