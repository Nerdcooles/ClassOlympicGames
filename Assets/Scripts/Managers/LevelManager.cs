using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameManager.eLevels level;
	public GameObject panel_instructions;
	public GameObject panel_countdown;
	public GameObject panel_podium;
	public GameObject panel_finish;
	public GameObject panel_pause;

	public bool withRound;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;
	private eState old_state;

	private Panel instructions;
	private Panel countdown;
	private Panel finish;
	private Panel podium;
	private Panel pause;

	public delegate void StateChange();
	public event StateChange OnCountdown;
	public event StateChange OnStart;
	public event StateChange OnPause;
	public event StateChange OnResume;
	public event StateChange OnFinish;

	private GameManager.ePlayers[] positions;
	private int num_players;

	void Awake() {
				if (GameManager.Instance.getNumPlayer () == 0) {
						Debug.Log ("Debug mode");
						GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
						GameManager.Instance.createPlayers (4);
						for (int i=0; i<4; i++)
								GameManager.Instance.setColor ((GameManager.ePlayers)i, (GameManager.eColors)i);
				}

				//INSTRUCTIONS
				instructions = panel_instructions.GetComponent<Panel> ();

				//COUNTDOWN
				countdown = panel_countdown.GetComponent<Panel> ();

				//FINISH
				finish = panel_finish.GetComponent<Panel> ();

				//PODIUM
				podium = panel_podium.GetComponent<Panel> ();

				pause = panel_pause.GetComponent<Panel>();

				if (withRound) {
						RoundManager.Instance.Image = GameObject.Find ("Round").GetComponent<Image> ();
						RoundManager.Instance.Image.sprite = Resources.Load <Sprite> ("Sprites/Round/round_" + RoundManager.Instance.Round);
				}
	}

	void Start() {
		num_players = GameManager.Instance.getNumPlayer ();
		positions = new GameManager.ePlayers[num_players];
		for (int i=0; i<num_players; i++)
						positions [i] = GameManager.ePlayers.none;

		if (withRound && RoundManager.Instance.Round != 0) {
			ShowCountdown();
		} else {
			state = eState.Instructions;
			instructions.Show();
		}
	}
	
	public void ShowCountdown() {
		Time.timeScale=1;
		instructions.Hide ();
		state = eState.Countdown;
		if(OnCountdown != null)
			OnCountdown();
		countdown.Show ();
	}

	public void StartGame() {
		countdown.Hide ();
		state = eState.Run;
		if(OnStart != null)
			OnStart();
	}

	public void FinishGame() {
		if (withRound) {
			RoundManager.Instance.NextRound();
		}
		finish.Show();
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		finish.Hide();
		state = eState.Finish;
		if(OnFinish != null)
			OnFinish();
		for (int i=0; i<positions.Length; i++) {
			GameManager.Instance.addMedal (positions [i], (GameManager.eMedals)i);
		}

		yield return new WaitForSeconds(3f);
		if (withRound) {
						if (RoundManager.Instance.Round < 3) {
								Application.LoadLevel (Application.loadedLevel);
				yield return new WaitForSeconds(1f);
			} else {
								RoundManager.Instance.Reset ();
						}
				}
		podium.Show ();
	}

	public int GetPosition(GameManager.ePlayers player) {
		for (int i=0; i<positions.Length; i++) {
			if(positions[i] == player)
				return i;
		}
		throw new System.ArgumentException ("Player doesn't exist");
		}

	public void PauseGame() {
		Debug.Log ("Pause");
		if(old_state!=eState.Pause)
			old_state = state;
		
		if(OnPause != null)
			OnPause();
		state = eState.Pause;
		pause.Show();
		Time.timeScale=0;
	}

	public void ShowInstructions() {
		pause.Hide();
		instructions.Show();
	}

	public void ResumeGame() {
		Debug.Log ("Resume");
		if(OnResume != null)
			OnResume();
		state = old_state;
		pause.Hide();
		Time.timeScale=1;
	}

	public void LevelOver() {
		MenuManager.LevelOver();	
	}
	
	public void RestartGame() {
		Time.timeScale=1;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu() {
		Time.timeScale=1;
		MenuManager.NewGame ();
		}

	public GameManager.eLevels getLevel() {
		return level;
	}

	public void setPodium(GameManager.ePlayers player, int position) {
		positions [position] = player;
	}

	public GameManager.ePlayers getPodium(int position) {
				return positions [position];
		}

	public eState State {
		get {
			return state;
		}
	}

	public Panel Pause {
		get {
			return pause;
		}
	}
}
