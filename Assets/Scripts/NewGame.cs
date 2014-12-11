using UnityEngine;
using System;
using TouchScript.Gestures;


public class NewGame : MonoBehaviour {

	public GameObject classic;
	public GameObject training;
	public GameObject bestScores;

	private void OnEnable()
	{
		classic.GetComponent<TapGesture>().Tapped += loadClassic;
		training.GetComponent<TapGesture>().Tapped += loadTraining;
		bestScores.GetComponent<TapGesture>().Tapped += loadBestScores;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			classic.GetComponent<TapGesture>().Tapped -= loadClassic;
			training.GetComponent<TapGesture>().Tapped -= loadTraining;
			bestScores.GetComponent<TapGesture>().Tapped -= loadBestScores;
		}catch{}
	}
	
	private void loadClassic(object sender, EventArgs e)
	{
		Debug.Log("LOAD CLASSIC MODE");
		GameManager.Instance.startMode(GameManager.eGameMode.CLASSIC);
		GameManager.Instance.selectPlayers();
	}
	
	private void loadTraining(object sender, EventArgs e)
	{
		Debug.Log("LOAD TRAINING MODE");
		GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
		GameManager.Instance.selectPlayers();
	}
	
	private void loadBestScores(object sender, EventArgs e)
	{
		Debug.Log("LOAD BEST SCORES");
	}
	
}
