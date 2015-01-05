using UnityEngine;
using System.Collections;

public class Podium : MonoBehaviour {

	private LevelManager lvm;
	private SpriteRenderer first, second, third;

	private bool canSkip = false;
	private int secToSkip = 2;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		first = GameObject.Find("first").GetComponent<SpriteRenderer>();
		second = GameObject.Find("second").GetComponent<SpriteRenderer>();
		third = GameObject.Find("third").GetComponent<SpriteRenderer>();
		first.sprite = null;
		second.sprite = null;
		third.sprite = null;
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
			first.sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
		foreach (GameManager.ePlayers player in lvm.getSecondPlace())
			second.sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
		foreach (GameManager.ePlayers player in lvm.getThirdPlace())
			third.sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
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
