using UnityEngine;
using System.Collections;

public class ArcheryPencil : MonoBehaviour {

	public Sprite p1_sprite, p2_sprite, p3_sprite, p4_sprite;
	private GameManager.ePlayers player;
	private ArcheryLevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
	}
	
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

	public void setDirection(float dir) {
		transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "bound") {
			levelManager.newPencil(player);
			Destroy(gameObject);
		}
	}
}
