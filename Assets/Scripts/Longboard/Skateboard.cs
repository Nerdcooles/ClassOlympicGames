using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour {

	public GameManager.ePlayers player;
	GameObject player_gameobj;
	float y, z;

	void Start() {
		if(GameManager.Instance.IsPlaying(player)) {
			player_gameobj = GameObject.Find(player.ToString());
			y = player_gameobj.transform.position.y -57f;
			z = player_gameobj.transform.position.z;
		}else{
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if (player_gameobj.transform.position.x < -150f) {
			Vector3 newPos = new Vector3 (player_gameobj.transform.position.x, y, z);
			transform.position = newPos;
		}
	}
}
