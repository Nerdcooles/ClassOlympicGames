using UnityEngine;
using System.Collections;

public class ArcheryPencil : MonoBehaviour {

	
	private GameManager.ePlayers player;
	private LevelManager lvm;
	private ArcheryLevelManager alvm;
	
	private bool shooted;
	float speed = 4f;
	float dir;
	
	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		alvm = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
		lvm.OnFinish += DestroyMe;
		dir = (Random.Range(0,2) - 0.5f) * speed;
		shooted = false;
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
	
	
	private void DestroyMe() {
		lvm.OnFinish -= DestroyMe;
		Destroy (gameObject);
	}
	
	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/ArcheryLevel/pen/" + GameManager.Instance.getColor(player));
	}
	
	public GameManager.ePlayers getPlayer() {
		return player;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "target") {
			DestroyMe();	
			alvm.Score(player);
		}
		if(other.name == "bound") {
			DestroyMe();
		}
		if (other.tag == "Bullet") {
			DestroyMe();
			Destroy (other.gameObject);
		}
	}

	public void shoot() {
		shooted = true;
	}
}
