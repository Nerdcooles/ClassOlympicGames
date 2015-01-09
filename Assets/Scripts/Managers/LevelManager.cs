using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameManager.eLevels level;
	public GameObject panel_instructions;
	public GameObject panel_podium;
	public GameObject panel_finish;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;

	private Instructions instructions;
	private Countdown countdown;
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
		instructions = panel_instructions.GetComponent<Instructions>();

		//COUNTDOWN
		countdown = GetComponentInChildren<Countdown>();

		//FINISH
		panel_finish.SetActive (false);
		//PODIUM
		podium = panel_podium.GetComponent<Podium> ();
	}

	void Start() {
		num_players = GameManager.Instance.getNumPlayer ();
		positions = new GameManager.ePlayers[num_players];

		instructions.Show();
		state = eState.Instructions;
		Debug.Log ("INSTRUCTIONS");
	}
	
	public void ShowCountdown() {
		instructions.Hide();
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
		panel_finish.SetActive (true);
		if(OnFinish != null)
			OnFinish();
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		panel_finish.SetActive (false);
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
