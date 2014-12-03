using UnityEngine;
using System.Collections;

public class ArcheryPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	public GameObject pencilPrefab;
	public float force = 300f;
	private GameObject pencil;
	private bool canShoot = false;

	void Start() {
		newPencil();
	}	

	public void setDirection(float dir) {
		if(canShoot)
		{		
			if(player == GameManager.ePlayers.p01 || player == GameManager.ePlayers.p04)
				pencil.GetComponent<ArcheryPencil>().setDirection(dir*60);
			  else 
			   pencil.GetComponent<ArcheryPencil>().setDirection(180-dir*60);
		}
	}

	public void shoot() {
		if(canShoot) {
			pencil.rigidbody2D.AddForce(pencil.transform.right * force);
			canShoot = false;
		}
	}

	public void newPencil() {
		pencil = Instantiate(pencilPrefab, transform.position, transform.rotation) as GameObject;
		pencil.GetComponent<ArcheryPencil>().setPlayer(player);
		canShoot = true;
	}

	


}
