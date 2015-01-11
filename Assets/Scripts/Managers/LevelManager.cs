﻿using UnityEngine;
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

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;

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
		if (GameManager.Instance.getNumPlayer() == 0) {
						Debug.Log("Debug mode");
						GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
						GameManager.Instance.createPlayers (4);
				}

		//INSTRUCTIONS
		instructions = panel_instructions.GetComponent<Image>();

		//COUNTDOWN
		countdown = panel_countdown.GetComponent<Countdown>();

		//FINISH
		finish = panel_finish.GetComponent<Image>();

		//PODIUM
		podium = panel_podium.GetComponent<Podium> ();
	}

	void Start() {
		num_players = GameManager.Instance.getNumPlayer ();
		positions = new GameManager.ePlayers[num_players];

		instructions.sprite = Resources.Load <Sprite> ("Sprites/Instructions/" + level.ToString());
		instructions.enabled = true;
		state = eState.Instructions;
		Debug.Log ("INSTRUCTIONS");
	}

	void Update() {
		if (state == eState.Instructions)
				if (Input.anyKeyDown)
						ShowCountdown ();
	}
	
	public void ShowCountdown() {
		instructions.enabled = false;
		state = eState.Countdown;
		Debug.Log ("COUNTDOWN");
		if(OnCountdown != null)
			OnCountdown();
		countdown.StartCountdown ();
	}

	public void StartGame() {
		state = eState.Run;
		Debug.Log ("RUN");
		if(OnStart != null)
			OnStart();
	}

	public void FinishGame() {
		state = eState.Finish;
		Debug.Log ("FINISH");
		finish.enabled = true;
		if(OnFinish != null)
			OnFinish();
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		finish.enabled = false;
		podium.Show ();
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
		return positions[position];
	}
}
