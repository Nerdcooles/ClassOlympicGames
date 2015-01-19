using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour {
	
	protected LevelManager lvm;
	protected Image img;

	private bool canSkip = false;
	private int secToSkip = 2;
	
	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		img = gameObject.GetComponent<Image>();
		img.enabled = false;
	}
	
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip)
			Skip();
	}
	
	protected virtual void Skip() {
	}
	
	public void Show(){
		PrepareToShow ();
		gameObject.GetComponent<Image>().enabled = true;
		InvokeRepeating ("WaitToSkip", 0.1f, 1);
	}
	
	
	protected virtual void PrepareToShow() {
	}

	public void Hide(){
		PrepareToHide();
		gameObject.GetComponent<Image>().enabled = false;
	}
	
	
	protected virtual void PrepareToHide() {
	}
	
	private void WaitToSkip() {
		secToSkip--;
		if (secToSkip < 0) {
			canSkip=true;
			CancelInvoke("WaitToSkip");
			
		}
	}
}
