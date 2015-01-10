using UnityEngine;
using TouchScript.Gestures;
using System;


public class Home : MonoBehaviour {

	public void StartGame()
	{
		Application.LoadLevel("SelectMode");
	}

	public void Credits()
	{
		Debug.Log ("Credits");
	}
}
