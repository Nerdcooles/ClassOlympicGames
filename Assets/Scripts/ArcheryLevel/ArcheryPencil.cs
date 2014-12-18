using UnityEngine;
using System.Collections;

public class ArcheryPencil : MonoBehaviour {

	public Sprite p1_sprite, p2_sprite, p3_sprite, p4_sprite;
	private GameManager.ePlayers player;
	private ArcheryLevelManager levelManager;
	private bool shooted;
	float speed = 4f;
	float dir;
	
	void Start () {
		dir = (Random.Range(0,2) - 0.5f) * speed;
		shooted = false;
		levelManager = GameObject.Find("LevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
	}
	
	void Update () {
		if(shooted)
			transform.position = transform.position + transform.up * speed * Time.deltaTime;
		else{
			if(transform.rotation.z > 0.5f)
				dir = dir * -1;
			transform.Rotate (Vector3.forward * dir);
		}
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

	public void shoot() {
		shooted = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Bound") 
			Destroy(gameObject);
		else if(other.tag == "Bullet") {
			Destroy(gameObject);
			Destroy(other.gameObject);
		} else if(other.tag == "Target") {
			levelManager.score(this.player);
			Destroy(gameObject);
		}
	}
}
