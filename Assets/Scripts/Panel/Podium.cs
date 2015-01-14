using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Podium : MonoBehaviour {
	public GameObject[] pod;

	private LevelManager lvm;
	private RuntimeAnimatorController animCtrl;

	private bool canSkip = false;
	private int secToSkip = 2;

	
	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		gameObject.SetActive (false);
		GetComponent<Image> ().enabled = true;
	}
	
	void Update() {
		if(lvm.getState() == LevelManager.eState.Finish)
			if((Input.touchCount > 0 || Input.anyKey) && canSkip)
				MenuManager.LevelOver();
	}

	public void Show() {
		gameObject.SetActive (true);
		int num_players = GameManager.Instance.getNumPlayer ();
		for (int i=0; i<num_players; i++) {
			if(i==num_players-1 && i!=0) {
				//LOSER
				animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (lvm.getPodium(i)) + "_podium_loser");
			}else{
				//WINNER
				animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (lvm.getPodium(i)) + "_podium_winner");
			}
			pod[i].GetComponent<Animator>().runtimeAnimatorController = animCtrl;
			pod[i].SetActive(true);		
		}
		gameObject.SetActive (true);

		InvokeRepeating ("WaitToSkip", 0.1f, 1);
	}
	
	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			canSkip=true;
			CancelInvoke("WaitToSkip");
			
		}
	}
}
