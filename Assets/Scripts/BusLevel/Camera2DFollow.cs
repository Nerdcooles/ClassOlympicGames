using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {

	GameObject[] player;

	void Start() {
		player = new GameObject[4];
		for (int i=0; i<4; i++) {
			try{
				player [i] = GameObject.Find ("p0" + (i + 1));
			}catch(System.NullReferenceException e){
				//player disabled
			}
				}
	}
	
	void Update () {
		float max_x = 0;
		foreach (GameObject p in player) {
			try{
				if (p.transform.position.x > max_x) 
				max_x = p.transform.position.x;
			}catch(System.NullReferenceException e){
				//player disabled
			}
				
		}

		Vector3 newPos = new Vector3 (max_x, 0, -10f);
		transform.position = Vector3.Lerp (transform.position, newPos, 10 * Time.deltaTime);
	}
}
