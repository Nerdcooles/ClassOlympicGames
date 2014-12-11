using UnityEngine;
using System.Collections;

public class ScreenAdaptor : MonoBehaviour {


	public GameObject[] p1_fixed;
	public GameObject p1_btns;
	
	public GameObject[] p2_fixed;
	public GameObject p2_btns;
	
	public GameObject[] p3_fixed;
	public GameObject p3_btns;
	
	public GameObject[] p4_fixed;
	public GameObject p4_btns;

	public float size_x;
	
	void Start () {
		int num_players = GameManager.Instance.getNumPlayer();
		switch(num_players) {
			case 1: disable2p();
					disable3p();
					disable4p();
					shift1p();
					break;

			case 2: disable3p();
					disable4p();
					shift2p();
					break;
			
			case 3: disable4p();
					shift3p();
					break;
		}
		
	}

	private void disable2p() {
		p2_btns.SetActive(false);
		foreach(GameObject obj in p2_fixed)
			obj.SetActive(false);
	}
	
	private void disable3p() {
		p3_btns.SetActive(false);
		foreach(GameObject obj in p3_fixed)
			obj.SetActive(false);
	}
	
	private void disable4p() {
		p4_btns.SetActive(false);
		foreach(GameObject obj in p4_fixed)
			obj.SetActive(false);
	}

	
	private void shift1p() {
		Vector3 _pos = p1_btns.transform.position;
		_pos.x = _pos.x + (size_x/2f);
		p1_btns.transform.position = _pos;
	}

	private void shift2p() {
		Vector3 _pos1 = p1_btns.transform.position;
		_pos1.x = _pos1.x - (size_x/2f);
		Vector3 _pos2 = p2_btns.transform.position;
		_pos2.x = _pos2.x + (size_x/2f);
		p1_btns.transform.position = _pos1;
		p2_btns.transform.position = _pos2;
	}
	
	private void shift3p() {
		Vector3 _pos1 = p1_btns.transform.position;
		_pos1.x = _pos1.x - (size_x/2f);
		Vector3 _pos2 = p2_btns.transform.position;
		_pos2.x = _pos2.x - (size_x/2f);
		Vector3 _pos3 = p3_btns.transform.position;
		_pos3.x = _pos3.x - (size_x/2f);
		p1_btns.transform.position = _pos1;
		p2_btns.transform.position = _pos2;
		p3_btns.transform.position = _pos3;
	}
}
