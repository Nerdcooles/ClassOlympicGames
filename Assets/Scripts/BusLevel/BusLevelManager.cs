using UnityEngine;
using System.Collections;

public class BusLevelManager : MonoBehaviour {
		
	private int player_pos;
	private float start_time;
	private float finish_time;
	private int num_players;
	private LevelManager lvm;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		player_pos = 0;
		start_time = 0;
		finish_time = 0;
	}
	
	public int Finish(GameManager.ePlayers player) {
		Debug.Log (player.ToString ());
		return 0;
	}
}
