using UnityEngine;
using System.Collections;

public class BucketBall : MonoBehaviour {

	private GameManager.ePlayers player;
	private BucketLevelManager levelManager;
	private bool inTrash;
	
	void Start () {
		inTrash = false;
		levelManager = GameObject.Find("LevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
	}

	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
	}

	public GameManager.ePlayers getPlayer() {
		return player;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "trash" && !inTrash) {
			inTrash = true;
			levelManager.score(this.player);
			GetComponent<Animator>().enabled = false;
			transform.localScale = new Vector3(1f, 1f, 1f);		
		}
		if(other.name == "bound") {
			Destroy(gameObject);
		}
	}
}
