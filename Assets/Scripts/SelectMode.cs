using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectMode : MonoBehaviour {

	public GameObject classic_btn;
	public Sprite classic_btn_released;
	public Sprite classic_btn_pressed;

	public GameObject training_btn;
	public Sprite training_btn_released;
	public Sprite training_btn_pressed;

	public GameObject score_btn;
	public Sprite score_btn_released;
	public Sprite score_btn_pressed;
//
	
	void Awake() {
		classic_btn.GetComponent<SpriteRenderer>().sprite = classic_btn_released;
		training_btn.GetComponent<SpriteRenderer>().sprite = training_btn_released;
		score_btn.GetComponent<SpriteRenderer>().sprite = score_btn_released;
	}
	
	private void OnEnable()
	{
		classic_btn.GetComponent<PressGesture>().Pressed += ClassicPressed;
		classic_btn.GetComponent<ReleaseGesture>().Released += ClassicReleased;
		training_btn.GetComponent<PressGesture>().Pressed += TrainingPressed;
		training_btn.GetComponent<ReleaseGesture>().Released += TrainingReleased;
		score_btn.GetComponent<PressGesture>().Pressed += ScorePressed;
		score_btn.GetComponent<ReleaseGesture>().Released += ScoreReleased;
	}


//	
	private void OnDisable()
	{
		try{
			classic_btn.GetComponent<PressGesture>().Pressed -= ClassicPressed;
			classic_btn.GetComponent<ReleaseGesture>().Released -= ClassicReleased;
			training_btn.GetComponent<PressGesture>().Pressed -= TrainingPressed;
			training_btn.GetComponent<ReleaseGesture>().Released -= TrainingReleased;
			score_btn.GetComponent<PressGesture>().Pressed -= ScorePressed;
			score_btn.GetComponent<ReleaseGesture>().Released -= ScoreReleased;
		}catch{}
	}

	void ClassicPressed (object sender, EventArgs e)
	{
		classic_btn.GetComponent<SpriteRenderer>().sprite = classic_btn_pressed;
	}

	void ClassicReleased (object sender, EventArgs e)
	{
		classic_btn.GetComponent<SpriteRenderer>().sprite = classic_btn_released;
		
		Debug.Log("LOAD CLASSIC MODE");
		GameManager.Instance.startMode(GameManager.eGameMode.CLASSIC);
		GameManager.Instance.selectPlayers();
	}
	
	void TrainingPressed (object sender, EventArgs e)
	{
		training_btn.GetComponent<SpriteRenderer>().sprite = training_btn_pressed;
	}
	
	void TrainingReleased (object sender, EventArgs e)
	{
		training_btn.GetComponent<SpriteRenderer>().sprite = training_btn_released;
		
		Debug.Log("LOAD TRAINNG MODE");
		GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
		GameManager.Instance.selectPlayers();
	}
	
	void ScorePressed (object sender, EventArgs e)
	{
		score_btn.GetComponent<SpriteRenderer>().sprite = score_btn_pressed;
	}
	
	void ScoreReleased (object sender, EventArgs e)
	{
		score_btn.GetComponent<SpriteRenderer>().sprite = score_btn_released;
		
		Debug.Log("LOAD BEST SCORE");
	}

	
}
