using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour {

	public GameObject[] floors;

	void Start () {
		switch (GameManager.Instance.getNumPlayer ()) {
		case 1: floors[0].SetActive(false); floors[1].SetActive(false);floors[3].SetActive(false);break; 
		case 2: floors[0].SetActive(false); floors[3].SetActive(false);break;
		case 3: floors[0].SetActive(false); break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
