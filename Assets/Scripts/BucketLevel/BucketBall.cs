using UnityEngine;
using System.Collections;

public class BucketBall : MonoBehaviour {

	private GameManager.ePlayers player;
	private LevelManager lvm;
	private BucketLevelManager blvm;

	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		blvm = GameObject.Find("BucketLevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
		lvm.OnFinish += DestroyMe;
	}


	private void DestroyMe() {
		lvm.OnFinish -= DestroyMe;
		Destroy (gameObject);
		}

	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
	}

	public GameManager.ePlayers getPlayer() {
		return player;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "trash") {
			DestroyMe();	
			blvm.Score(player);
		}
		if(other.tag == "Bound") {
			DestroyMe();
		}
	}
}
