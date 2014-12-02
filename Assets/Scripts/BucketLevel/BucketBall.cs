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
		switch(player) {
		case GameManager.ePlayers.p01: gameObject.GetComponent<SpriteRenderer>().sprite = p1_sprite; break;
		case GameManager.ePlayers.p02: gameObject.GetComponent<SpriteRenderer>().sprite = p2_sprite; break;
		case GameManager.ePlayers.p03: gameObject.GetComponent<SpriteRenderer>().sprite = p3_sprite; break;
		case GameManager.ePlayers.p04: gameObject.GetComponent<SpriteRenderer>().sprite = p4_sprite; break;
			
		}
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
