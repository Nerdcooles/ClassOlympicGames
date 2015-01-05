using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameManager.eLevels level;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;

	private Instructions instructions;
	private Countdown countdown;
	private Podium podium;

	public delegate void StateChange();
	public event StateChange OnStart;
	public event StateChange OnFinish;

	private List<GameManager.ePlayers> firstPlace;
	private List<GameManager.ePlayers> secondPlace;
	private List<GameManager.ePlayers> thirdPlace;

	void Awake() {
		if (GameManager.Instance.getNumPlayer() == 0) {
						Debug.Log("Debug mode");
						GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
						GameManager.Instance.createPlayers (4);
				}

		//INSTRUCTIONS
		instructions = GetComponentInChildren<Instructions> ();

		//COUNTDOWN
		countdown = GetComponentInChildren<Countdown>();

		//PODIUM
		podium = GetComponentInChildren<Podium> ();
	}

	void Start() {
		instructions.Show();
		state = eState.Instructions;
		Debug.Log ("INSTRUCTIONS");
	}
	
	public void ShowCountdown() {
		instructions.Hide();
		state = eState.Countdown;
		Debug.Log ("COUNTDOWN");
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
		if(OnFinish != null)
			OnFinish();
		podium.Show ();
	}

	public eState getState() {
		return state;
	}

	public GameManager.eLevels getLevel() {
		return level;
	}

	public void setFirstPlace(List<GameManager.ePlayers> players) {
		this.firstPlace = players;
	}
	
	public void setSecondPlace(List<GameManager.ePlayers> players) {
		this.secondPlace = players;
	}
	
	public void setThirdPlace(List<GameManager.ePlayers> players) {
		this.thirdPlace = players;
	}
	public List<GameManager.ePlayers> getFirstPlace() {
		return firstPlace;
	}
	
	public List<GameManager.ePlayers> getSecondPlace() {
		return secondPlace;
	}

	public List<GameManager.ePlayers> getThirdPlace() {
		return thirdPlace;
	}

}
