using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections;


public class ArcheryPlayer : MonoBehaviour {
	
	public GameManager.ePlayers player;
	public GameObject shoot_btn;
	public GameObject pencilPrefab;
	
	private float force;
	private float press_time;
	private Vector3 direction;
	private GameObject pencilInstance;
	private bool shooted;
	private bool can_reload;
	
	
	// Use this for initialization
	void Start () {
		shooted = false;
	}

	void Update() {
		if(pencilInstance==null || (shooted && can_reload)){
			pencilInstance = Instantiate(pencilPrefab, transform.position + transform.up * -0.4f, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-60f, 60f))) as GameObject;
			pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			StopAllCoroutines();
			shooted = false;
			can_reload = false;
		}
	}
	
	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		shoot_btn.GetComponent<TapGesture>().Tapped += shoot;
		
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			shoot_btn.GetComponent<TapGesture>().Tapped -= shoot;
		}
		catch
		{
		}
	}

	
	private void shoot(object sender, EventArgs e) {
		if(!shooted && pencilInstance!=null) {
			pencilInstance.GetComponent<ArcheryPencil>().shoot();
			StartCoroutine(Reload());
			shooted = true;
		}
	}

	IEnumerator Reload() {
		yield return new WaitForSeconds(1f);
		can_reload = true;
	}
}

//	public GameManager.ePlayers player;
//	public GameObject pencilPrefab;
//	public float force = 300f;
//	private GameObject pencil;
//	private bool canShoot = false;
//
//	void Start() {
//		newPencil();
//	}	
//
//	public void setDirection(float dir) {
//		if(canShoot)
//		{		
//			if(player == GameManager.ePlayers.p01 || player == GameManager.ePlayers.p04)
//				pencil.GetComponent<ArcheryPencil>().setDirection(dir*60);
//			  else 
//			   pencil.GetComponent<ArcheryPencil>().setDirection(180-dir*60);
//		}
//	}
//
//	public void shoot() {
//		if(canShoot) {
//			pencil.rigidbody2D.AddForce(pencil.transform.right * force);
//			canShoot = false;
//		}
//	}
//
//	public void newPencil() {
//		pencil = Instantiate(pencilPrefab, transform.position, transform.rotation) as GameObject;
//		pencil.GetComponent<ArcheryPencil>().setPlayer(player);
//		canShoot = true;
//	}
