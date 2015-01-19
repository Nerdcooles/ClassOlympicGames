using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HudKeyboardAdapter : MonoBehaviour {

	public GameObject[] button;

	private BtnHandler[] btn;
		
	void Start() {
		int num_players = GameManager.Instance.getNumPlayer ();

		btn = new BtnHandler[num_players];
		for (int i=0; i<num_players; i++) {
			btn[i] = button[i].GetComponent<BtnHandler>() as BtnHandler;
		}
	}

	void Update()
	{
			if (Input.GetButtonDown ("Player1")){
				btn[0].Press(null,null);
			}
			if (Input.GetButtonUp ("Player1")) {
				btn[0].Release(null,null);
			}

			if (Input.GetButtonDown ("Player2")){
				btn[1].Press(null,null);
			}
			if (Input.GetButtonUp ("Player2")) {
				btn[1].Release(null,null);
			}

			if (Input.GetButtonDown ("Player3")){
				btn[2].Press(null,null);
			}
			if (Input.GetButtonUp ("Player3")) {
				btn[2].Release(null,null);
			}

			if (Input.GetButtonDown ("Player4")){
				btn[3].Press(null,null);
			}
			if (Input.GetButtonUp ("Player4")) {
				btn[3].Release(null,null);
			}
	}

	public GameObject[] Button {
		get {
			return button;
		}
	}
}
