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
	public GameObject btn_pause;

	public bool withRound;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;
	private eState old_state;

	private Image instructions;
	private Countdown countdown;
	private Image finish;
	private Podium podium;

	public delegate void StateChange();
	public event StateChange OnCountdown;
	public event StateChange OnStart;
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
				instructions = panel_instructions.GetComponent<Image> ();

				//COUNTDOWN
				countdown = panel_countdown.GetComponent<Countdown> ();

				//FINISH
				finish = panel_finish.GetComponent<Image> ();

				//PODIUM
				podium = panel_podium.GetComponent<Podium> ();

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
						instructions.sprite = Resources.Load <Sprite> ("Sprites/Instructions/" + level.ToString ());
						instructions.enabled = true;
						state = eState.Instructions;
		}
	}


	void Update() {
		if (state == eState.Instructions)
				if (Input.anyKeyDown)
						ShowCountdown ();
	}
	
	public void ShowCountdown() {
		panel_instructions.SetActive (false);
		btn_pause.SetActive (true);
		state = eState.Countdown;
		if(OnCountdown != null)
			OnCountdown();
		countdown.StartCountdown ();
	}

	public void StartGame() {
		panel_countdown.SetActive (false);
		state = eState.Run;
		if(OnStart != null)
			OnStart();
	}

	public void FinishGame() {
		if (withRound) {
			RoundManager.Instance.NextRound();
		}
		panel_finish.SetActive (true);
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		finish.enabled = false;
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
		Debug.Log ("Ask position " + player.ToString ());
		for (int i=0; i<positions.Length; i++) {
			Debug.Log ("Pos " + i + " " + positions[i].ToString());
			if(positions[i] == player)
				return i;
		}
		throw new System.ArgumentException ("Player doesn't exist");
		}

	public void PauseGame() {
		Debug.Log ("Pause");
		old_state = state;
		state = eState.Pause;
		btn_pause.SetActive (false);
		panel_pause.SetActive (true);
		Time.timeScale=0;
	}

	public void ResumeGame() {
		Debug.Log ("Resume");
		state = old_state;
		panel_pause.SetActive (false);
		btn_pause.SetActive (true);
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

	public eState getState() {
		return state;
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
}
