using UnityEngine;
using TouchScript.Gestures;
using System;


public class Home : MonoBehaviour {

	Transform canvas;

	void Start() {
		canvas = GameObject.Find("Canvas").transform;

	}
	public void StartGame()
	{
		MenuManager.SelectMode();
	}

	public void Credits()
	{
		MenuManager.Credits();
	}

	public void Exit() {
		GameObject exit_panel = Instantiate(Resources.Load<GameObject>("Panels/Exit")) as GameObject;
		exit_panel.transform.SetParent(canvas, false);
	}
}
