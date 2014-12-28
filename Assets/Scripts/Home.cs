using UnityEngine;
using TouchScript.Gestures;
using System;


public class Home : MonoBehaviour {

	public GameObject start_btn;
	public Sprite start_btn_released;
	public Sprite start_btn_pressed;
	
	public GameObject credits_btn;
	public Sprite credits_btn_released;
	public Sprite credits_btn_pressed;

	
	void Awake() {
		start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_released;
		credits_btn.GetComponent<SpriteRenderer>().sprite = credits_btn_released;
	}
	
	private void OnEnable()
	{
		start_btn.GetComponent<PressGesture>().Pressed += startPressed;
		start_btn.GetComponent<ReleaseGesture>().Released += startReleased;
		credits_btn.GetComponent<PressGesture>().Pressed += creditsPressed;
		credits_btn.GetComponent<ReleaseGesture>().Released += creditsReleased;
	}

	private void OnDisable()
	{
		try{
			start_btn.GetComponent<PressGesture>().Pressed -= startPressed;
			start_btn.GetComponent<ReleaseGesture>().Released -= startReleased;
			credits_btn.GetComponent<PressGesture>().Pressed -= creditsPressed;
			credits_btn.GetComponent<ReleaseGesture>().Released -= creditsReleased;
		}catch{}
	}

	void startPressed (object sender, EventArgs e)
	{
		start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_pressed;
	}

	void startReleased (object sender, EventArgs e)
	{
		//start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_released;
		MenuManager.selectMode();
	}
	
	void creditsPressed (object sender, EventArgs e)
	{
		credits_btn.GetComponent<SpriteRenderer>().sprite = credits_btn_pressed;
	}
	
	void creditsReleased (object sender, EventArgs e)
	{
		//credits_btn.GetComponent<SpriteRenderer>().sprite = credits_btn_released;
		MenuManager.credits();
	}
	
}
