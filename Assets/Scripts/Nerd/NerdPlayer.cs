﻿using UnityEngine;
using System.Collections;

public class NerdPlayer : LevelPlayer {
	
	private NerdLevelManager sceneMgr;

	private GameObject nerdPrefab;
	private float force;
	private Vector3 direction;
	private GameObject nerdInstance;



	bool shooted = false;

	protected override void Initialize() {
		sceneMgr = GameObject.Find("NerdLevelManager").GetComponent<NerdLevelManager>() as NerdLevelManager;
		nerdPrefab = Resources.Load <GameObject> ("Prefabs/Nerd");
	}

	void StartPlayer() {
		nerdInstance = Instantiate(nerdPrefab, transform.position - new Vector3(20f, 20f, 0), Quaternion.Euler(new Vector3(0, 0, -25f))) as GameObject;
		nerdInstance.GetComponent<Nerd> ().Player = player;
		animator.SetBool("isLoading", true);
	}
	

	protected override void Pressed() {
		if(lvm.State == LevelManager.eState.Run && !shooted) {
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
		}
	}

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		try{
			shooted = true;
			nerdInstance.GetComponent<Nerd>().Shooted = true;
			nerdInstance.GetComponent<Nerd>().StartPt = transform.position.x;
		}catch{
		}
		yield return new WaitForSeconds(1f);
	}
	
	public void endShooting() {
		animator.SetBool("isShooting",false);
	}

}
