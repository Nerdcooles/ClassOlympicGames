using UnityEngine;
using System.Collections;

public class AwardManager : MonoBehaviour {

	void Update() {
		if (Input.anyKeyDown)
			Skip ();
	}
	public void Skip() {
		MenuManager.NewGame ();
	}
}
