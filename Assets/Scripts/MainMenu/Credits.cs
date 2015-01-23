using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	void Update() {
		if(Input.touchCount > 0 || Input.anyKey) {
			MenuManager.NewGame();
		}
	}
}
