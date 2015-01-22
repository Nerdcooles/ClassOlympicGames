using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	LevelManager lvm;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
	}

	public void Continue() {
		lvm.ResumeGame();
	}
	
	public void Tutorial() {
		lvm.ShowInstructions();
	}
	
	public void Restart() {
		lvm.RestartGame();
	}
	
	public void MainMenu() {
		lvm.MainMenu();
	}

	public void Exit() {
		GameObject exit_panel = Instantiate(Resources.Load<GameObject>("Panels/Exit")) as GameObject;
		exit_panel.transform.SetParent(transform.parent.transform, false);
		exit_panel.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
	}


}
