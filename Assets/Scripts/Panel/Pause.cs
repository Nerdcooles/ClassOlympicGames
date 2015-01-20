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

	public void Exit() {
		GameObject exit_panel = Instantiate(Resources.Load<GameObject>("Panels/Exit")) as GameObject;
		exit_panel.transform.SetParent(transform.parent.transform, false);
	}
}
