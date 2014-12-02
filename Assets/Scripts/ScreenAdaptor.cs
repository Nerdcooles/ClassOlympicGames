using UnityEngine;
using System.Collections;

public class ScreenAdaptor : MonoBehaviour {

	public GameObject[] p1;
	public GameObject[] p2;
	public GameObject[] p3;
	public GameObject[] p4;
	
	void Start () {
		int num_players = GameManager.Instance.getNumPlayer();
		switch(num_players) {
			case 1: foreach(GameObject obj in p2)
						obj.SetActive(false);
					goto case 2;
			case 2: foreach(GameObject obj in p3)
						obj.SetActive(false);
					goto case 3;
			case 3: foreach(GameObject obj in p4)
						obj.SetActive(false);
					break;
		}
		
	}
}
