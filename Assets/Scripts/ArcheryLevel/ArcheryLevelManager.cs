using UnityEngine;
using System.Collections;

public class ArcheryLevelManager : MonoBehaviour {

	public GameObject[] players;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void newPencil(GameManager.ePlayers pl) {
		players[pl.GetHashCode()].GetComponent<ArcheryPlayer>().newPencil();
	}
}
