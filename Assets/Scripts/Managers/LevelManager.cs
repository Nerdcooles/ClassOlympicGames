using UnityEngine;
using System;
using System.Collections;
using TouchScript.Gestures;

public class LevelManager : MonoBehaviour {

	protected GameManager.eLevels level;
	public int num_players = 4;

	protected LevelUI levelUI;
	protected bool start;
	protected bool gameover;
	
	private bool pressed_to_skip;

	protected void PrepareLevel(GameManager.eLevels lv) {
		start = false;
		gameover = false;
		levelUI = GameObject.Find("LevelUI").GetComponent<LevelUI>() as LevelUI;
		#region DEBUG
		if(GameManager.Instance.getNumPlayer()==0) {
			GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
			GameManager.Instance.createPlayers(num_players);
			MenuManager.startLevel(lv);
		}
		#endregion
		num_players = GameManager.Instance.getNumPlayer();
		StartCoroutine(Countdown());
	}
	
	protected IEnumerator Countdown() {
		Debug.Log("COUNTDOWN");
		levelUI.show(LevelUI.ePanel.Countdown);
		Debug.Log("3");
		levelUI.countdown(3);
		yield return new WaitForSeconds(1f);
		Debug.Log("2");
		levelUI.countdown(2);
		yield return new WaitForSeconds(1f);
		Debug.Log("1");
		levelUI.countdown(1);
		yield return new WaitForSeconds(1f);
		Debug.Log("GO");
		levelUI.countdown(0);
		yield return new WaitForSeconds(1f);
		levelUI.hide(LevelUI.ePanel.Countdown);
		LetsGo();
	}
	
	protected virtual void LetsGo() {
		start = true;
	}

	
	#region FINAL_PANEL
	private void OnEnable()
	{
		levelUI.getPanel(LevelUI.ePanel.Scores).GetComponent<TapGesture>().Tapped += pressed;
	}

	
	private void OnDisable()
	{
		try{
			levelUI.getPanel(LevelUI.ePanel.Scores).GetComponent<TapGesture>().Tapped -= pressed;
		}catch{}
	}

	void pressed (object sender, EventArgs e) {
		pressed_to_skip = true;
	}

	protected IEnumerator GameOver() {
		Debug.Log("GAME OVER");
		gameover = true;
		levelUI.show(LevelUI.ePanel.Scores);
		pressed_to_skip = false;
		yield return StartCoroutine(WaitForSkip());
		pressed_to_skip = false;
		if(GameManager.Instance.getGameMode()==GameManager.eGameMode.CLASSIC) {
			levelUI.hide(LevelUI.ePanel.Scores);
			levelUI.show(LevelUI.ePanel.Podium);
			
			pressed_to_skip = false;
			yield return StartCoroutine(WaitForSkip());
			pressed_to_skip = false;

			levelUI.hide(LevelUI.ePanel.Podium);
			levelUI.show(LevelUI.ePanel.Medals);
			
			pressed_to_skip = false;
			yield return StartCoroutine(WaitForSkip());
			pressed_to_skip = false;
		}
		MenuManager.gameOver(this.level);
	}

	private bool _keyPressed = false;
	
	public IEnumerator WaitForSkip()
	{
		while(!_keyPressed)
		{
			if(pressed_to_skip)
			{
				_keyPressed = true;
				break;
			}
			yield return 0;
		}
	}
	#endregion FINAL_PANEL

	public bool isStarted() {
		return start;
	}
	
	public bool isGameover() {
		return gameover;
	}
}
