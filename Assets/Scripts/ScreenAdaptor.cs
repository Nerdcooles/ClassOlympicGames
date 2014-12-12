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
		_pos.x = 0.0f;
		p1_btns.transform.position = _pos;

		Vector3 _scale = new Vector3(4f,1,1);
		p1_btns.transform.localScale = _scale;
		
	}

	private void shift2p() {
		Vector3 _pos1 = p1_btns.transform.position;
		_pos1.x = -3.5f;
		p1_btns.transform.position = _pos1;

		Vector3 _pos2 = p2_btns.transform.position;
		_pos2.x = 3.5f;
		p2_btns.transform.position = _pos2;

		Vector3 _scale = new Vector3(2f,1,1);
		p1_btns.transform.localScale = _scale;
		p2_btns.transform.localScale = _scale;
	}
	
	private void shift3p() {
		Vector3 _pos1 = p1_btns.transform.position;
		_pos1.x = -4.45f;
		p1_btns.transform.position = _pos1;
		
		Vector3 _pos2 = p2_btns.transform.position;
		_pos2.x = 0.0f;
		p2_btns.transform.position = _pos2;
		
		Vector3 _pos3 = p3_btns.transform.position;
		_pos3.x = +4.45f;
		p3_btns.transform.position = _pos3;

		Vector3 _scale = new Vector3(1.33f,1,1);
		p1_btns.transform.localScale = _scale;
		p2_btns.transform.localScale = _scale;
		p3_btns.transform.localScale = _scale;
		
	}
}
