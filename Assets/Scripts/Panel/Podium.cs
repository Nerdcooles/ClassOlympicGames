using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Podium : MonoBehaviour {

	public GameObject[] pod = new GameObject[3];

	private LevelManager lvm;
	private RuntimeAnimatorController animCtrl;

	private bool canSkip = false;
	private int secToSkip = 1;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		int num_players = GameManager.Instance.getNumPlayer ();
		int nc = 0;
		for (int position=0; position<pod.Length; position++) {
			try {
				GameManager.ePlayers player = lvm.getPodium(position);
				if(player == GameManager.ePlayers.none)
					nc++;

				if(position==num_players-1 && position!=0) {
					//LOSER
					animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_loser");
				}else{
					//WINNER
					animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + GameManager.Instance.getColor (player) + "_podium_winner");
				}
				pod[position].GetComponent<Animator>().runtimeAnimatorController = animCtrl;
				pod[position].SetActive(true);
				GameObject medal = Resources.Load<GameObject> ("Prefabs/Medal_" + position);
				medal.GetComponent<MedalMovement>().enabled = true;
				GameObject medlaInstance = Instantiate (medal) as GameObject;
				medlaInstance.transform.SetParent(gameObject.transform, false);
				medlaInstance.transform.position = pod[position].transform.position;
			}catch{
			}
		}

		if(nc == 3)
			GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Podium/bg_podium_nc");
		InvokeRepeating ("WaitToSkip", 0.1f, 1f);

	}
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip)
			lvm.LevelOver();
	}

	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			canSkip=true;
			CancelInvoke("WaitToSkip");
			
		}
	}
}
