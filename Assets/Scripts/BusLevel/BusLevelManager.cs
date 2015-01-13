using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BusLevelManager : MonoBehaviour {
		
	public GameObject[] player;

	private int player_pos;
	private float start_time;
	private float first_time;
	private int num_players;
	private LevelManager lvm;
	private List<GameManager.ePlayers> playersToFinish;

	private Vector3[] init_pos;

	public const float WAIT_SECS = 10f;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		init_pos = new Vector3[4];
		for (int i=0; i<4; i++)
						init_pos[i] = player [i].transform.position;
	}

	void Start() {
		player_pos = 0;
		start_time = 0;
		first_time = 0;
		num_players = GameManager.Instance.getNumPlayer ();
		playersToFinish = GameManager.Instance.getPlayers ();

		switch (num_players) {
				case 1:
			player [0].transform.position = init_pos [2];
			
						break;
				case 2:
			player [0].transform.position = init_pos [1];
			player [1].transform.position = init_pos [2];

						break;
				case 3:
			player [0].transform.position = init_pos [1];
			player [1].transform.position = init_pos [2];
			player [2].transform.position = init_pos [3];
						break;
				}
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
		GameManager.Instance.addMedal (player, (GameManager.eMedals)player_pos);
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
		foreach (GameManager.ePlayers p in playersToFinish) {
						lvm.setPodium (p, player_pos++);
				}
		if(lvm.getState()!=LevelManager.eState.Finish)
		Finish ();
	}

	void Finish() {
		CancelInvoke ("WatiLastPlayer");
		lvm.FinishGame ();
	}
}
