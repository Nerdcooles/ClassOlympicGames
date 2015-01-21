using UnityEngine;
using System.Collections;

public class ArcheryPencil : MonoBehaviour {

	
	private GameManager.ePlayers player;
	private LevelManager lvm;
	private ArcheryLevelManager alvm;

	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		alvm = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
	}
	
	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/ArcheryLevel/pen/" + GameManager.Instance.getColor(player));
	}
	
	public GameManager.ePlayers getPlayer() {
		return player;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Target") {
			other.GetComponent<ArcheryTeacher>().Hitted();
			Destroy(gameObject);
			alvm.Score(player);
		}
		if(other.tag == "Bound") {
			Destroy(gameObject);
		}
		if (other.tag == "Bullet") {
			Destroy(gameObject);
			Destroy (other.gameObject);
		}
	}
}
