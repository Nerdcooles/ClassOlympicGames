using UnityEngine;
using System.Collections;

public class ArcheryTeacher : MonoBehaviour {

	private LevelManager lvm;
	float speed = 150f;

	private Animator animator;

	void Awake () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		animator = GetComponent<Animator>();			
	}
	
	void OnEnable()
	{
		lvm.OnStart += Move;
		lvm.OnFinish += Finish;
	}
	
	
	void OnDisable()
	{
		lvm.OnStart -= Move;
		lvm.OnFinish -= Finish;
		
	}
	
	void Move() {
		animator.SetBool ("isMoving", true);
		rigidbody2D.AddForce(new Vector3(Random.Range(0,2) * 2 - 1, 0f, 0f) * speed);
	}

	void Hitted() {
		animator.SetBool ("isHitted", true);
		rigidbody2D.velocity = Vector2.zero;
	}

	public void EndHitted() {
		animator.SetBool ("isHitted", false);
		Move ();
	}

	void Finish() {
		rigidbody2D.velocity = Vector2.zero;
		animator.SetBool ("isMoving", false);
		}
	
	void RemoveCollider() {
		GetComponent<BoxCollider2D> ().enabled = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Bullet")
						Hitted ();
		if (other.gameObject.tag == "EnemyBound") {
			Vector2 vel = rigidbody2D.velocity;
			vel.x *= -1;
			rigidbody2D.velocity = vel;
		}
	}
}
