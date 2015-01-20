using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BusLevelManager : MonoBehaviour {
		
	private int player_pos;
	private float start_time;
	private float first_time;
	private int num_players;
	private LevelManager lvm;
	private List<GameManager.ePlayers> playersToFinish;
	
	public const float WAIT_SECS = 10f;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		MusicManager.Instance.Source.Stop ();
		MusicManager.Instance.Source.clip = Resources.Load<AudioClip>(MusicManager.songs[0]);
		MusicManager.Instance.Source.Play ();
	}

	void Start() {
		player_pos = 0;
		start_time = 0;
		first_time = 0;
		num_players = GameManager.Instance.getNumPlayer ();
		playersToFinish = GameManager.Instance.getPlayers ();

	}
	void OnEnable()
	{
		lvm.OnStart += StartTimer;
	}
	
	
	void OnDisable()
	{
		lvm.OnStart -= StartTimer;
	}
	
	void StartTimer() {
		start_time = Time.time;
	}
	
	public int Score(GameManager.ePlayers player) {
		lvm.setPodium (player, player_pos);
		playersToFinish.Remove (player);

		if (player_pos == 0) {
						StartCoroutine ("WaitLastPlayer");
				}
		if (player_pos == num_players - 1) {
						Finish ();
				}
		return player_pos++;
	}

	IEnumerator WaitLastPlayer() {
		yield return new WaitForSeconds(WAIT_SECS);
		if (lvm.State != LevelManager.eState.Finish) {
						Finish ();
				}
	}

	void Finish() {
		CancelInvoke ("WatiLastPlayer");
		lvm.FinishGame ();
	}
}
