using UnityEngine;
using System.Collections;

public class BusLevelManager : MonoBehaviour {
		
	public GameObject firsLine;

	private int player_pos;
	private float start_time;
	private float finish_time;
	private int num_players;
	private LevelManager lvm;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		num_players = GameManager.Instance.getNumPlayer ();
		firsLine.SetActive (false);
		player_pos = 0;
		start_time = 0;
		finish_time = 0;
	}

	void OnEnable()
	{
		lvm.OnStart += ActiveFirstLine;
	}
	
	
	void OnDisable()
	{
		lvm.OnStart -= ActiveFirstLine;
	}

	void ActiveFirstLine() {
		firsLine.SetActive (true);
	}
	public int Finish(GameManager.ePlayers player) {
//		finish_time = Time.time - start_time;
//		//levelUI.score(player, finish_time.ToString("0.00") + " sec");
//		if(player_pos < 3){
//			GameManager.Instance.addMedal(player, (GameManager.eMedals)player_pos);
//		//	levelUI.medal(player, (GameManager.eMedals)player_pos);
//		}
//		player_pos++;
//		if(player_pos >= num_players)
////			StartCoroutine(GameOver());
//		return player_pos-1;
		return 0;
	}
}
