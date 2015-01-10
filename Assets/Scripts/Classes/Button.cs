using UnityEngine;
using System;
using System.Collections;
using TouchScript.Gestures;

public class Button : MonoBehaviour {
	
	public delegate void Gesture();
	public event Gesture OnPressed;
	public event Gesture OnReleased;

	public GameManager.ePlayers player;
	public KeyCode key;
	private GameManager.eColors color;

	private Sprite s_pressed;
	private Sprite s_released;

	private const float BTN_Y = -3.5f;
	private const float BTN_Z = -8f;
	private const float BTN_CENTER = 0f;
	private const float BTN_DOBLE = 2.5f;
	private const float BTN_TRIPLE = 4.5f;

	private LevelManager lvm;
	private bool enabled;

	void Awake() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		enabled = false;
	}

	void Start() {
		try {
			color = GameManager.Instance.getColor(player);
			s_released = Resources.Load <Sprite> ("Sprites/Buttons/" + color.ToString() + "_" + player.ToString());
			s_pressed = Resources.Load <Sprite> ("Sprites/Buttons/" + color.ToString() + "_" + player.ToString() + "_pressed");
			gameObject.GetComponent<SpriteRenderer>().sprite = s_released;
		}catch{
			gameObject.SetActive(false);
		}
		AdaptPosition();
	}

	void Update() {
		if (Input.GetKeyDown (key) && enabled) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = s_pressed;

						if (OnPressed != null) {

								OnPressed ();
						}
				}
		if (Input.GetKeyUp (key) && enabled) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = s_released;	

						if (OnReleased != null) {
								OnReleased ();
						}
				}
		}

	private void OnEnable()
	{
		gameObject.GetComponent<PressGesture>().Pressed += Pressed;
		gameObject.GetComponent<ReleaseGesture>().Released += Released;
		lvm.OnStart += EnableButton;
		lvm.OnFinish += DisableButton;
	}
	
	private void OnDisable()
	{
			gameObject.GetComponent<PressGesture>().Pressed -= Pressed;
			gameObject.GetComponent<ReleaseGesture>().Released -= Released;	
		lvm.OnStart -= EnableButton;
		lvm.OnFinish -= DisableButton;
	}

	void EnableButton() {
		enabled = true;
	}
	
	void DisableButton() {
		enabled = false;
	}

	private void Pressed(object sender, EventArgs e) {
		if (enabled) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = s_pressed;
						if (OnPressed != null)
								OnPressed ();
				}
	}
	
	private void Released(object sender, EventArgs e) {
		if (enabled) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = s_released;	
						if (OnReleased != null)
								OnReleased ();	
				}
	}

	private void AdaptPosition() {
		int num = GameManager.Instance.getNumPlayer();
		switch(num){
			case 1: switch(player) {
						case GameManager.ePlayers.p01: gameObject.transform.position = new Vector3(BTN_CENTER, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p02: gameObject.SetActive(false); break;
						case GameManager.ePlayers.p03: gameObject.SetActive(false); break;
						case GameManager.ePlayers.p04: gameObject.SetActive(false); break;
			} break;
			case 2: switch(player) {
						case GameManager.ePlayers.p01: gameObject.transform.position = new Vector3(-BTN_DOBLE, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p02: gameObject.transform.position = new Vector3(BTN_DOBLE, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p03: gameObject.SetActive(false); break;
						case GameManager.ePlayers.p04: gameObject.SetActive(false); break;
			} break;
			case 3: switch(player) {
						case GameManager.ePlayers.p01: gameObject.transform.position = new Vector3(-BTN_TRIPLE, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p02: gameObject.transform.position = new Vector3(BTN_CENTER, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p03: gameObject.transform.position = new Vector3(BTN_TRIPLE, BTN_Y, BTN_Z); break;
						case GameManager.ePlayers.p04: gameObject.SetActive(false); break;
				} break;
		}
	}
}
