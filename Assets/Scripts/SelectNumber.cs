using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectNumber : MonoBehaviour {
	
	public void Start1p()
	{
		MenuManager.selectPlayer(1);
	}	

	public void Start2p()
	{
		MenuManager.selectPlayer(2);
	}	

	public void Start3p()
	{
		MenuManager.selectPlayer(3);
	}	

	public void Start4p()
	{
		MenuManager.selectPlayer(4);
	}		
	
	public void Back ()
	{
		MenuManager.selectMode();
	}
}
