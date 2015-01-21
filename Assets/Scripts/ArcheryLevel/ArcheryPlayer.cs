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
	private bool can_shoot = true;
	private bool pressed = false;


	
	protected override void Initialize() {
		sceneMgr = GameObject.Find("ArcheryLevelManager").GetComponent<ArcheryLevelManager>() as ArcheryLevelManager;

		if(transform.rotation.y != 0)
			pencilPosX = -pencilPosX;

			pencilPrefab = Resources.Load <GameObject> ("Prefabs/ArcheryPencil");
		lvm.OnTimeIsUp += RemovePencilInstances;

		animator.SetBool("isHappy", true);
	}

	protected override void Pressed() {
		if(lvm.State == LevelManager.eState.Run && can_shoot && pencilInstance==null) {
			pressed = true;
			can_shoot = false;
			pencilInstance = Instantiate(pencilPrefab, transform.position + new Vector3(pencilPosX, pencilPosY, 0f) , Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f))) as GameObject;
			pencilInstance.GetComponent<ArcheryPencil>().setPlayer(player);
			direction = (UnityEngine.Random.Range(0,2) * 2 - 1);
			press_time = Time.time;	
			animator.SetBool("isLoading", true);
			CancelInvoke ("SpinPencil");
			InvokeRepeating ("SpinPencil", 0.1f, 0.01f);
		}
	}
	
	void SpinPencil() {
		if(lvm.State != LevelManager.eState.Run)
			return;
		pencilInstance.transform.Rotate (direction*Vector3.forward, Time.deltaTime * 180, Space.Self);
	}
	
	protected override void Released() {
		if(lvm.State == LevelManager.eState.Run && pressed) {
			pressed = false;
			CancelInvoke("SpinPencil");
			animator.SetBool("isLoading", false);
			animator.SetBool("isShooting", true);
			StartCoroutine(waitAnimation());
			can_shoot = true;
		}
	}

	private IEnumerator waitAnimation() {
		yield return new WaitForSeconds(0.1f);
			pencilInstance.rigidbody2D.AddForce(pencilInstance.transform.right * 500, ForceMode2D.Impulse);
			pencilInstance = null;
	}
	
	public void endShooting() {
		animator.SetBool("isShooting",false);
	}
	
	public void endHitted() {
		animator.SetBool("isHitted",false);
		animator.SetBool("isLoading",false);
		animator.SetBool("isShooting",false);
		gameObject.collider2D.enabled = true;
		can_shoot=true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Bullet" && other.gameObject.GetComponent<ArcheryPencil>().getPlayer() != player) {
			can_shoot=false;
			pressed = false;
			gameObject.collider2D.enabled = false;
			Destroy(other.gameObject);
			Destroy(pencilInstance);
			animator.SetBool("isHitted",true);
			
			CancelInvoke ("SpinPencil");
		}
	}

	void RemovePencilInstances() {
		Destroy(pencilInstance);
	}
}
