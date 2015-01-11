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

	private Sprite[] pressed;
	private Sprite[] released;
	private Image[] img;
		
	void Start() {
		int num_players = GameManager.Instance.getNumPlayer ();
		switch (num_players) {
		case 1: key_p1 = KEY_MID; break;
		case 2: key_p1 = KEY_LEFT; key_p2 = KEY_RIGHT; break;
		case 3: key_p1 = KEY_LEFT; key_p2 = KEY_MID; key_p3 = KEY_RIGHT; break;
		case 4: key_p1 = KEY_LEFT; key_p2 = KEY_MID_LEFT; key_p3 = KEY_MID_RIGHT; key_p4 = KEY_RIGHT; break;
		}

		pressed = new Sprite[num_players];
		released = new Sprite[num_players];
		img = new Image[num_players];

		for (int i= 0; i<num_players; i++) {
			pressed[i] = button[i].GetComponent<Button>().spriteState.pressedSprite;
			img[i] = button[i].GetComponent<Image>();
			released[i] = img[i].sprite;
		}
	}
	void Update()
	{
		var pointer = new PointerEventData(EventSystem.current);

		if (key_p1 != null) {
			if (Input.GetKeyDown (key_p1)){
				img[0].sprite = pressed[0];
			}
			if (Input.GetKeyUp (key_p1)) {
				img[0].sprite = released[0];
			}
		}
		
		if (key_p2 != null) {
			if (Input.GetKeyDown (key_p2)){
				img[1].sprite = pressed[1];
			}
			if (Input.GetKeyUp (key_p2)) {
				img[1].sprite = released[1];
			}
		}
		
		if (key_p3 != null) {
			if (Input.GetKeyDown (key_p3)){
				img[2].sprite = pressed[2];
			}
			if (Input.GetKeyUp (key_p3)) {
				img[2].sprite = released[2];
			}
		}
		
		if (key_p4 != null) {
			if (Input.GetKeyDown (key_p4)){
				img[3].sprite = pressed[3];
			}
			if (Input.GetKeyUp (key_p4)) {
				img[3].sprite = released[3];
			}
		}
	}

	public GameObject[] Button {
		get {
			return button;
		}
	}
}
