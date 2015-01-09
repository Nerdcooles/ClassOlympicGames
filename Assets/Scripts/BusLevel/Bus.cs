using UnityEngine;
using System.Collections;

public class Bus : MonoBehaviour {
	private LevelManager lvm;
	float init_speed = 30f;
	float speed = 80f;
	float turbo = 2f;

	void Awake () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	void OnEnable()
	{
		lvm.OnCountdown += InitBus;
		lvm.OnStart += MoveBus;
	}
	
	
	void OnDisable()
	{
		lvm.OnCountdown -= InitBus;
		lvm.OnStart -= MoveBus;
	}
	
	void InitBus() {
		rigidbody2D.AddForce(Vector3.right * init_speed);
	}

	void MoveBus() {
		rigidbody2D.AddForce(Vector3.right * speed);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
			rigidbody2D.AddForce(Vector3.right * speed * turbo);
		if (other.gameObject.tag == "Target") {
			transform.position = transform.position + new Vector3(1f, 0, 0);
			collider2D.isTrigger = false;
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}
