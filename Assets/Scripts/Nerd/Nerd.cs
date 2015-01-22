using UnityEngine;
using System.Collections;

public class Nerd : MonoBehaviour {
	
	
	private GameManager.ePlayers player;
	private LevelManager lvm;
	private NerdLevelManager nlvm;
	private bool shooted = false;
	private bool idle = true;
	private int direction = 1;
	private float startPt;

	float dir;
	
	void Start () {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		nlvm = GameObject.Find("NerdLevelManager").GetComponent<NerdLevelManager>() as NerdLevelManager;
		rigidbody2D.gravityScale = 0;
	}

	void Update() {
		if (rigidbody2D.velocity != Vector2.zero) {
			Quaternion dir = Quaternion.LookRotation(rigidbody2D.velocity);
			transform.rotation = Quaternion.Euler (0f, 0f, -90 + (dir.x)*(-180f));
				}
		if (shooted) {
			rigidbody2D.gravityScale = 100;
			rigidbody2D.AddForce(transform.up * 850, ForceMode2D.Impulse);
			shooted = false;
			idle = false;
		}
		if (idle) {
			float degree = transform.rotation.eulerAngles.z;
			if(degree < 280 && degree > 270 ) 
				direction = 1;
			if(degree < 10 && degree > 0 ) 
				direction = -1;
			transform.Rotate (direction * Vector3.forward, Time.deltaTime * 180, Space.Self);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "floor"+(player.GetHashCode()+1)) {
			rigidbody2D.gravityScale = 0f;
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.angularVelocity = 0f; 

			Animator animator = GetComponent<Animator>();			
			animator.SetBool("onFloor", true);
			nlvm.Score(player, transform.position.x - startPt);
		}
	}

	public GameManager.ePlayers Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}

	public bool Shooted {
		get {
			return shooted;
		}
		set {
			shooted = value;
		}
	}

	public float StartPt {
		get {
			return startPt;
		}
		set {
			startPt = value;
		}
	}
}
