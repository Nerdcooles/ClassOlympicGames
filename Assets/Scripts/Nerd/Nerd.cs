using UnityEngine;
using System.Collections;

public class Nerd : MonoBehaviour {
	
	
	private GameManager.ePlayers player;
	private LevelManager lvm;
	private NerdLevelManager alvm;
	private bool shooted = false;

	float dir;
	
	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		alvm = GameObject.Find("NerdLevelManager").GetComponent<NerdLevelManager>() as NerdLevelManager;
		rigidbody2D.gravityScale = 0;
		//transform.Rotate (Vector3.forward, Time.deltaTime * 180, Space.Self);
	}

	void Update() {
		if (shooted) {
			rigidbody2D.gravityScale = 100;
			rigidbody2D.AddForce(transform.up * 900, ForceMode2D.Impulse);
			shooted = false;
		}
	}

	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "floor"+(player.GetHashCode()+1)) {
			rigidbody2D.velocity = Vector2.zero;
		}
	}


	public bool Shooted {
		get {
			return shooted;
		}
		set {
			shooted = value;
		}
	}
}
