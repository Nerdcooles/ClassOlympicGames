using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectPlayers : MonoBehaviour {

	public GameObject p1;
	public GameObject p2;
	public GameObject p3;
	public GameObject p4;

	void Start() {
		switch(GameManager.Instance.getGameMode()) {
		case GameManager.eGameMode.CLASSIC: p1.SetActive(false); break;
		case GameManager.eGameMode.TRAINING: p1.SetActive(true); break;
		}
	}

	private void OnEnable()
	{
		p1.GetComponent<TapGesture>().Tapped += start1;
		p2.GetComponent<TapGesture>().Tapped += start2;
		p3.GetComponent<TapGesture>().Tapped += start3;
		p4.GetComponent<TapGesture>().Tapped += start4;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			p1.GetComponent<TapGesture>().Tapped -= start1;
			p2.GetComponent<TapGesture>().Tapped -= start2;
			p3.GetComponent<TapGesture>().Tapped -= start3;
			p4.GetComponent<TapGesture>().Tapped -= start4;
		}catch{}
	}
	
	private void start1(object sender, EventArgs e)
	{
		Debug.Log("START 1 PLAYER");
		GameManager.Instance.startGame(1);
	}
	
	private void start2(object sender, EventArgs e)
	{
		Debug.Log("START 2 PLAYERS");
		GameManager.Instance.startGame(2);
	}
	
	private void start3(object sender, EventArgs e)
	{
		Debug.Log("START 3 PLAYERS");
		GameManager.Instance.startGame(3);
	}
	
	private void start4(object sender, EventArgs e)
	{
		Debug.Log("START 4 PLAYERS");
		GameManager.Instance.startGame(4);
	}
}
