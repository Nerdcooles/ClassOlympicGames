using UnityEngine;
using System.Collections;

public class Podium : MonoBehaviour {

	private LevelManager lvm;
	
	private bool canSkip = false;
	private int secToSkip = 2;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}
	
	void Start() {
		gameObject.SetActive (false);
	}
	
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip)
			GameManager.Instance.LevelOver(lvm.getLevel());
	}

	public void Show() {
		foreach (GameManager.ePlayers player in lvm.getFirstPlace())
			Debug.Log ("first p" + player.ToString ());
		foreach (GameManager.ePlayers player in lvm.getSecondPlace())
			Debug.Log ("second p" + player.ToString ());
		foreach (GameManager.ePlayers player in lvm.getThirdPlace())
			Debug.Log ("third p" + player.ToString ());
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
