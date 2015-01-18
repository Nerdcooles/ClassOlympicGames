using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections;

public class ArcheryPlayer : LevelPlayer {

	private ArcheryLevelManager sceneMgr;

	private float pencilPosX = -32.6f;
	private float pencilPosY = -9f;
	private GameObject pencilPrefab;
	
	private float force;
	private float press_time;
	private int direction;
	private GameObject pencilInstance;
	private bool can_shoot;


	
	protected override void Initialize() {
		sceneMgr = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;

		can_shoot = true;
		if(transform.rotation.y != 0)
			pencilPosX = -pencilPosX;

			pencilPrefab = Resources.Load <GameObject> ("Prefabs/ArcheryPencil");

	}

	protected override void Pressed() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			pencilInstance = Instantiate(pencilPrefab, transform.position + new Vector3(pencilPosX, pencilPosY, 0f) , Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f))) as GameObject;
			pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			direction = (UnityEngine.Random.Range(0,2) * 2 - 1);
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			animator.SetBool("isShooting", false);
			InvokeRepeating ("SpinPencil", 0.1f, 0.01f);
		}
	}
	
	void SpinPencil() {
		try{
			pencilInstance.transform.Rotate (direction*Vector3.forward, Time.deltaTime * 180, Space.Self);
		}catch{
				}
	}
	
	protected override void Released() {
		if(lvm.getState() == LevelManager.eState.Run && can_shoot) {
			CancelInvoke("SpinPencil");
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
		}
	}

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
		try{
		pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			pencilInstance.rigidbody2D.AddForce(pencilInstance.transform.right * 500, ForceMode2D.Impulse);
		}catch{
		}
		yield return new WaitForSeconds(1f);
	}
	
	public void endShooting() {
		animator.SetBool("isShooting",false);
	}
	
	public void endHitted() {
		animator.SetBool("isHitted",false);
		animator.SetBool("isLoading",false);
		press_time = Time.time;	
		can_shoot=true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<ArcheryPencil>().getPlayer() != player) {
			can_shoot=false;
			Destroy(other.gameObject);
			Destroy(pencilInstance);
			animator.SetBool("isHitted",true);

		}
	}
}
