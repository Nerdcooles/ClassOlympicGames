using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

	GameObject[] player;

	void Start() {
		player = new GameObject[4];
		for (int i=0; i<4; i++)
			player[i] = GameObject.Find ("Player" + (i+1));
	}
	
	void Update () {
		float max_x = 0;
		foreach (GameObject p in player) {
						if (p.transform.position.x > max_x) {
								max_x = p.transform.position.x;
						}
				}

		Vector3 newPos = new Vector3 (max_x, 0, -10f);
		transform.position = Vector3.Lerp (transform.position, newPos, 10 * Time.deltaTime);
	}
}
