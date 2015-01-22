﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameManager.eLevels level;

	private Transform panels;
	private GameObject panel_instructions;
	private GameObject panel_podium;
	private GameObject panel_pause;

	private GameObject pause_btn;

	private Image finish;
	private Image countdown;

	private GameObject instructionPrefab;
	private GameObject pausePrefab;
	private GameObject podiumPrefab;

	public bool withRound;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;
	private eState old_state;
	
	public delegate void StateChange();
	public event StateChange OnCountdown;
	public event StateChange OnStart;
	public event StateChange OnPause;
	public event StateChange OnResume;
	public event StateChange OnTimeIsUp;
	public event StateChange OnFinish;
	public event StateChange OnShowMedals;

	private GameManager.ePlayers[] positions;
	private int num_players;
	private int initial_countdown = 3;

	void Awake() {
				if (GameManager.Instance.getNumPlayer () == 0) {
						Debug.Log ("Debug mode");
						GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
						GameManager.Instance.createPlayers (4);
						for (int i=0; i<4; i++)
								GameManager.Instance.setColor ((GameManager.ePlayers)i, (GameManager.eColors)i);
				}
				panels = GameObject.Find("Panels").transform;
				countdown = GameObject.Find("Countdown").GetComponent<Image>();
				countdown.enabled = false;
				finish = GameObject.Find("Finish").GetComponent<Image>();
				finish.enabled = false;

				pausePrefab = Resources.Load<GameObject>("Panels/Pause");
				instructionPrefab = Resources.Load<GameObject>("Instructions/" + level.ToString());
				podiumPrefab = Resources.Load<GameObject>("Panels/Podium");

				pause_btn = GameObject.Find("Pause_btn");

				if (withRound) {
						RoundManager.Instance.Image = GameObject.Find ("Round").GetComponent<Image> ();
						RoundManager.Instance.Image.sprite = Resources.Load <Sprite> ("Sprites/Round/round_" + RoundManager.Instance.Round);
				}
	}

	void Start() {
		num_players = GameManager.Instance.getNumPlayer ();
		positions = new GameManager.ePlayers[num_players];
		for (int i=0; i<positions.Length; i++) {
			positions[i] = GameManager.ePlayers.none;
		}
		if (withRound && RoundManager.Instance.Round != 0) {
			ShowCountdown();
		} else {
			state = eState.Instructions;
			ShowInstructions();
		}
	}
	
	public void ShowCountdown() {
		Time.timeScale=1;
		state = eState.Countdown;
		if(OnCountdown != null)
			OnCountdown();
		countdown.enabled = true;
		InvokeRepeating ("CountDown", 0.1f, 0.8f);

	}

	private void CountDown() {
		if(initial_countdown<0) {
			countdown.enabled = false;
			StartGame();
			CancelInvoke("CountDown");
			return;
		}
		countdown.sprite = Resources.Load <Sprite> ("Sprites/Common/countdown_" + initial_countdown);
		initial_countdown--;
	}

	public void StartGame() {
		countdown.enabled = false;
		state = eState.Run;
		if(OnStart != null)
			OnStart();
	}

	public void FinishGame() {
		state = eState.Finish;
		if(OnTimeIsUp != null)
			OnTimeIsUp();
		if (withRound) {
			RoundManager.Instance.NextRound();
		}
		finish.enabled = true;
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		finish.enabled = false;
		if(OnFinish != null)
			OnFinish();
		for (int i=0; i<positions.Length; i++) {
			if(positions[i]!=GameManager.ePlayers.none)
				GameManager.Instance.addMedal (positions [i], (GameManager.eMedals)i);
		}
		yield return new WaitForSeconds(1f);
		if(OnShowMedals!=null)
			OnShowMedals();
		yield return new WaitForSeconds(3f);
		if (withRound) {
						if (RoundManager.Instance.Round < 3) {
								Application.LoadLevel (Application.loadedLevel);
				yield return new WaitForSeconds(1f);
			} else {
								RoundManager.Instance.Reset ();
						}
				}
		ShowPodium();
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
		panel_pause = Instantiate(pausePrefab) as GameObject;
		panel_pause.transform.SetParent(panels, false);
		Time.timeScale=0;
	}

	public void ShowInstructions() {
		panel_instructions = Instantiate(instructionPrefab) as GameObject;
		panel_instructions.transform.SetParent(panels, false);
	}
	
	public void SkipInstructions() {
		Destroy(panel_instructions);
		if(state == eState.Instructions)
			ShowCountdown();
	}
	
	public void ShowPodium() {
		pause_btn.SetActive(false);
		panel_podium = Instantiate(podiumPrefab) as GameObject;
		panel_podium.transform.SetParent(panels, false);
	}

	public void ResumeGame() {
		Debug.Log ("Resume");
		if(OnResume != null)
			OnResume();
		state = old_state;
		Destroy(panel_pause);
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
		if(player != GameManager.ePlayers.none)
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
}
