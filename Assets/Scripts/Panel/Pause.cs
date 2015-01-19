using UnityEngine;
using System.Collections;

public class Pause : Panel {

	protected override void PrepareToShow() {
		foreach (Transform child in transform)
		{
				child.gameObject.SetActive(true);
		}
	}
	
	protected override void PrepareToHide() {
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(false);
		}
	}


}
