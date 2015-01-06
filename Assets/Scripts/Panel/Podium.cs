using UnityEngine;
using System.Collections;

public class Podium : MonoBehaviour {

	private LevelManager lvm;

	private bool canSkip = false;
	private int secToSkip = 2;
	private GameObject podiumPrefab;
	
	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		podiumPrefab = Resources.Load<GameObject> ("Prefabs/Podium");
	}
	
	void Start() {
		gameObject.SetActive (false);
	}
	
	void Update() {
		if((Input.touchCount > 0 || Input.anyKey) && canSkip)
			GameManager.Instance.LevelOver(lvm.getLevel());
	}

	public void Show() {
		int i = 0;
		foreach (GameManager.ePlayers player in lvm.getFirstPlace()) {
			GameObject pl = Instantiate(podiumPrefab, transform.position + new Vector3(0.3f + i*0.5f,0.8f,0), transform.rotation) as GameObject;
			pl.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
			i++;
		}

		i = 1;
		foreach (GameManager.ePlayers player in lvm.getSecondPlace()) {
			GameObject pl = Instantiate(podiumPrefab, transform.position + new Vector3(1 + i*0.5f,0.5f,0), transform.rotation) as GameObject;
			pl.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
			i++;
		}

		i = -1;
		foreach (GameManager.ePlayers player in lvm.getThirdPlace()) {
			GameObject pl = Instantiate(podiumPrefab, transform.position + new Vector3(i*0.5f,0.2f,0), transform.rotation) as GameObject;
			pl.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Podium/" + GameManager.Instance.getColor(player));
			i--;
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
