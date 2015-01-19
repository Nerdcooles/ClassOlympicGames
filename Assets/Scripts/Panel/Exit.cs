using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public void Yes() {
		Application.Quit();
	}

	
	public void No() {
		Destroy(gameObject);
	}
}
