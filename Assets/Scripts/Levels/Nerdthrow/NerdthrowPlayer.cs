using UnityEngine;using System.Collections;/** * PC in Nerd Throw */public class NerdthrowPlayer : PlayerCharacter{	private GameObject nerdPrefab;	private float force;	private Vector3 direction;	private GameObject nerdInstance;	bool shooted = false;	protected override void Initialize ()	{		nerdPrefab = Resources.Load <GameObject> ("Prefabs/Nerd");	}	protected override void OnCountdown ()	{		nerdInstance = Instantiate (nerdPrefab, transform.position - new Vector3 (20f, 20f, 0), Quaternion.Euler (new Vector3 (0, 0, UnityEngine.Random.Range (280, 350)))) as GameObject;		nerdInstance.GetComponent<NerdthrowNerd> ().Player = player;		animator.SetBool ("isLoading", true);	}	protected override void Pressed ()	{		if (lvm.State == LevelManager.eState.Run && !shooted) {			animator.SetBool ("isLoading", false);			animator.SetBool ("isShooting", true);			StartCoroutine (waitAnimation ());		}	}	private IEnumerator waitAnimation ()	{		try {			shooted = true;			nerdInstance.GetComponent<NerdthrowNerd> ().Shooted = true;			nerdInstance.GetComponent<NerdthrowNerd> ().StartPt = transform.position.x;		} catch {		}		yield return new WaitForSeconds (1f);	}		public void endShooting ()	{		animator.SetBool ("isShooting", false);	}}