using UnityEngine;
using System.Collections;

public class Bus : MonoBehaviour {
	private LevelManager lvm;
	public float init_speed = 30f;
	public float speed = 80f;
	public float turbo = 2f;

	void Awake () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	void OnEnable()
	{
		lvm.OnCountdown += InitBus;
		lvm.OnStart += MoveBus;
		lvm.OnFinish += RemoveCollider;
	}
	
	
	void OnDisable()
	{
		lvm.OnCountdown -= InitBus;
		lvm.OnStart -= MoveBus;
		lvm.OnFinish -= RemoveCollider;

	}
	
	void InitBus() {
		rigidbody2D.AddForce(Vector3.right * init_speed);
	}

	void MoveBus() {
		rigidbody2D.AddForce(Vector3.right * speed);
	}

	void RemoveCollider() {
		GetComponent<BoxCollider2D> ().enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player")
				rigidbody2D.AddForce(Vector3.right * speed * turbo);
		if (other.gameObject.tag == "Target") {
			transform.position = transform.position + new Vector3(1f, 0, 0);
			collider2D.isTrigger = false;
			RemoveCollider();
			rigidbody2D.velocity = Vector2.zero;
		}
	}
}
