using UnityEngine;
using System.Collections;

public class GenericMenu : MonoBehaviour {
	
	Transform canvas;
	
	void Start() {
		GameManager.Instance.CurrentMenu = Application.loadedLevel;
		canvas = GameObject.Find("Canvas").transform;
	}
	
	
	public void Exit() {
		GameObject exit_panel = Instantiate(Resources.Load<GameObject>("Panels/Exit")) as GameObject;
		exit_panel.transform.SetParent(canvas, false);
	}

	public void Back() {
		Application.LoadLevel(GameManager.Instance.CurrentMenu-1);
	}

}
