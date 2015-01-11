using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HudKeyboardAdapter : MonoBehaviour {
	
	private const KeyCode KEY_LEFT = KeyCode.A;
	private const KeyCode KEY_MID_LEFT = KeyCode.F;
	private const KeyCode KEY_MID = KeyCode.G;
	private const KeyCode KEY_MID_RIGHT = KeyCode.J;
	private const KeyCode KEY_RIGHT = KeyCode.L;

	public GameObject[] button;

	private KeyCode key_p1;
	private KeyCode key_p2;
	private KeyCode key_p3;
	private KeyCode key_p4;

	private BtnHandler[] btn;
		
	void Start() {
		int num_players = GameManager.Instance.getNumPlayer ();
		switch (num_players) {
		case 1: key_p1 = KEY_MID; break;
		case 2: key_p1 = KEY_LEFT; key_p2 = KEY_RIGHT; break;
		case 3: key_p1 = KEY_LEFT; key_p2 = KEY_MID; key_p3 = KEY_RIGHT; break;
		case 4: key_p1 = KEY_LEFT; key_p2 = KEY_MID_LEFT; key_p3 = KEY_MID_RIGHT; key_p4 = KEY_RIGHT; break;
		}

		btn = new BtnHandler[num_players];
		for (int i=0; i<num_players; i++) {
			btn[i] = button[i].GetComponent<BtnHandler>() as BtnHandler;
		}
	}

	void Update()
	{
		if (key_p1 != null) {
			if (Input.GetKeyDown (key_p1)){
				btn[0].Press(null,null);
			}
			if (Input.GetKeyUp (key_p1)) {
				btn[0].Release(null,null);
			}
		}
		
		if (key_p2 != null) {
			if (Input.GetKeyDown (key_p2)){
				btn[1].Press(null,null);
			}
			if (Input.GetKeyUp (key_p2)) {
				btn[1].Release(null,null);
			}
		}
		
		if (key_p3 != null) {
			if (Input.GetKeyDown (key_p3)){
				btn[2].Press(null,null);
			}
			if (Input.GetKeyUp (key_p3)) {
				btn[2].Release(null,null);
			}
		}
		
		if (key_p4 != null) {
			if (Input.GetKeyDown (key_p4)){
				btn[3].Press(null,null);
			}
			if (Input.GetKeyUp (key_p4)) {
				btn[3].Release(null,null);
			}
		}
	}

	public GameObject[] Button {
		get {
			return button;
		}
	}
}
