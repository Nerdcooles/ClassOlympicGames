using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Podium : Panel {

	public GameObject[] pod = new GameObject[3];

	private RuntimeAnimatorController animCtrl;
		
	protected override void Skip() {
		lvm.LevelOver();
	}

	protected override void PrepareToShow() {
		int num_players = GameManager.Instance.getNumPlayer ();
		for (int i=0; i<pod.Length; i++) {
			try {
				GameManager.ePlayers player = lvm.getPodium(i);
				if(i==num_players-1 && i!=0) {
					//LOSER
					animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_loser");
				}else{
					//WINNER
					animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_winner");
				}
				pod[i].GetComponent<Animator>().runtimeAnimatorController = animCtrl;
				pod[i].SetActive(true);
			}catch{
				//player not classified
			}
		}
	}
}
