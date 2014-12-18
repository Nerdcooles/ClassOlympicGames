using UnityEngine;
using System.Collections;

public class BucketBall : MonoBehaviour {

	public Sprite p1_sprite, p2_sprite, p3_sprite, p4_sprite;

	private GameManager.ePlayers player;
	private BucketLevelManager levelManager;
	
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<BucketLevelManager>() as BucketLevelManager;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "trash") {
			levelManager.score(this.player);
			Destroy(gameObject);
		}
		if(other.name == "bound") {
			Destroy(gameObject);
		}
	}
}
