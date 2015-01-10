using UnityEngine;
using UnityEngine.UI;
using System;
using TouchScript.Gestures;


public class SelectNumber : MonoBehaviour {

	GameObject btn_1p;
	
	void Awake() {
		if(GameManager.Instance.getGameMode() == GameManager.eGameMode.TRAINING) {
			btn_1p = GameObject.Find ("btn_1p");
			btn_1p.GetComponent<Button>().interactable = false;
		}
	}

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
