using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public GameManager.eLevels level;

	private Transform panels;
	public GameObject panel_instructions;
	public GameObject panel_countdown;
	public GameObject panel_podium;
	public GameObject panel_finish;
	public GameObject panel_pause;

	private GameObject pausePrefab;
	private GameObject instructionPrefab;

	public bool withRound;

	public enum eState {Instructions, Countdown, Run, Pause, Finish}
	private eState state;
	private eState old_state;

	private Panel countdown;
	private Panel finish;
	private Panel podium;

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

	void Awake() {
				if (GameManager.Instance.getNumPlayer () == 0) {
						Debug.Log ("Debug mode");
						GameManager.Instance.startMode (GameManager.eGameMode.TRAINING);
						GameManager.Instance.createPlayers (4);
						for (int i=0; i<4; i++)
								GameManager.Instance.setColor ((GameManager.ePlayers)i, (GameManager.eColors)i);
				}
				panels = GameObject.Find("Panels").transform;

				pausePrefab = Resources.Load<GameObject>("Panels/Pause");
				instructionPrefab = Resources.Load<GameObject>("Instructions/" + level.ToString());
				//COUNTDOWN
				countdown = panel_countdown.GetComponent<Panel> ();

				//FINISH
				finish = panel_finish.GetComponent<Panel> ();

				//PODIUM
				podium = panel_podium.GetComponent<Panel> ();

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
		countdown.Show ();
	}

	public void StartGame() {
		countdown.Hide ();
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
		finish.Show();
		StartCoroutine ("WaitForPodium");
	}

	IEnumerator WaitForPodium() {
		yield return new WaitForSeconds(2f);
		finish.Hide();
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
		panel_pause = Instantiate(pausePrefab) as GameObject;
		panel_pause.transform.SetParent(panels, false);
		Time.timeScale=0;
	}

	public void ShowInstructions() {
		Time.timeScale=0;
		panel_instructions = Instantiate(instructionPrefab) as GameObject;
		panel_instructions.transform.SetParent(panels, false);
	}
	
	public void SkipInstructions() {
		Destroy(panel_instructions);
		if(state == eState.Instructions)
			ShowCountdown();
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
