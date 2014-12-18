using UnityEngine;
using System;
using TouchScript.Gestures;


public class Home : MonoBehaviour {

	public GameObject start_btn;
	public Sprite start_btn_released;
	public Sprite start_btn_pressed;

//	public GameObject classic;
//	public GameObject training;
//	public GameObject bestScores;
//
	
	void Awake() {
		start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_released;
	}
	
	private void OnEnable()
	{
		start_btn.GetComponent<PressGesture>().Pressed += Pressed;
		start_btn.GetComponent<ReleaseGesture>().Released += Released;
//		classic.GetComponent<TapGesture>().Tapped += loadClassic;
//		training.GetComponent<TapGesture>().Tapped += loadTraining;
//		bestScores.GetComponent<TapGesture>().Tapped += loadBestScores;
	}


//	
	private void OnDisable()
	{
		try{
			start_btn.GetComponent<PressGesture>().Pressed -= Pressed;
			start_btn.GetComponent<ReleaseGesture>().Released -= Released;
//			classic.GetComponent<TapGesture>().Tapped -= loadClassic;
//			training.GetComponent<TapGesture>().Tapped -= loadTraining;
//			bestScores.GetComponent<TapGesture>().Tapped -= loadBestScores;
		}catch{}
	}

	void Pressed (object sender, EventArgs e)
	{
		start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_pressed;
	}

	void Released (object sender, EventArgs e)
	{
		start_btn.GetComponent<SpriteRenderer>().sprite = start_btn_released;
		GameManager.Instance.selectMode();
	}

//	private void loadClassic(object sender, EventArgs e)
//	{
//		Debug.Log("LOAD CLASSIC MODE");
//		GameManager.Instance.startMode(GameManager.eGameMode.CLASSIC);
//		GameManager.Instance.selectPlayers();
//	}
//	
//	private void loadTraining(object sender, EventArgs e)
//	{
//		Debug.Log("LOAD TRAINING MODE");
//		GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
//		GameManager.Instance.selectPlayers();
//	}
//	
//	private void loadBestScores(object sender, EventArgs e)
//	{
//		Debug.Log("LOAD BEST SCORES");
//	}
	
}
