using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour {

	public GameObject player;

	void Start() {

	}

	void Update() {
		if (player.transform.position.x < -150f) {
						Vector3 newPos = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
						transform.position = newPos;
				}
	}
}
