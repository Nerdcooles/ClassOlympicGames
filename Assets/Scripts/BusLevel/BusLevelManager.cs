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

	public const float WAIT_SECS = 15f;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		player_pos = 0;
		start_time = 0;
		first_time = 0;
	}

	void Start() {
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
	
	public void Score(GameManager.ePlayers player) {
		Debug.Log ("Arrive " + player.ToString () + " POS " + player_pos);
		lvm.setPodium (player, player_pos);
		playersToFinish.Remove (player);

		if (player_pos == 0)
			StartCoroutine("WaitLastPlayer");
		if (player_pos == num_players-1)
			Finish ();

		player_pos++;
	}

	IEnumerator WaitLastPlayer() {
		yield return new WaitForSeconds(WAIT_SECS);
		foreach (GameManager.ePlayers p in playersToFinish) {
						lvm.setPodium (p, player_pos++);
			Debug.Log ("Doesn't arrive " + p.ToString ());

				}
		Finish ();
	}

	void Finish() {
		lvm.FinishGame ();
	}
}
