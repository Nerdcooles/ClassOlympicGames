using UnityEngine;
using System.Collections;

public class ArcheryPencil : MonoBehaviour {

	
	private GameManager.ePlayers player;
	private LevelManager lvm;
	private ArcheryLevelManager alvm;

	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		alvm = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;
		lvm.OnFinish += DestroyMe;
	}
	
	
	private void DestroyMe() {
		lvm.OnFinish -= DestroyMe;
		try{
			Destroy (gameObject);
		}catch{
		}	}
	
	public void setPlayer(GameManager.ePlayers player) {
		this.player = player;
		gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/ArcheryLevel/pen/" + GameManager.Instance.getColor(player));
	}
	
	public GameManager.ePlayers getPlayer() {
		return player;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
//		if(other.tag == "Target") {
//			other.GetComponent<ArcheryTeacher>().Hitted();
//			DestroyMe();	
//			alvm.Score(player);
//		}
//		if(other.tag == "Bound") {
//			DestroyMe();
//		}
//		if (other.tag == "Bullet") {
//			DestroyMe();
//			Destroy (other.gameObject);
//		}
	}
}
