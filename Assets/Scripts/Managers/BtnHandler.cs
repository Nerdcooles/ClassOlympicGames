using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TouchScript.Gestures;
using System;


public class BtnHandler : MonoBehaviour {
			
	public GameManager.ePlayers player;
	private GameManager.eColors color;
	private Image s_renderer;
	private Sprite s_pressed;
	private Sprite s_released;
	public delegate void Gesture();
	public event Gesture OnPressed;
	public event Gesture OnReleased;

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
		s_renderer = gameObject.GetComponent<Image> ();
			s_renderer.sprite = s_released;
		} catch {
			gameObject.SetActive (false);
		}
	}
	private void OnEnable()
	{
		gameObject.GetComponent<PressGesture>().Pressed += Press;
		gameObject.GetComponent<ReleaseGesture>().Released += Release;
		lvm.OnStart += EnableButton;
		lvm.OnFinish += DisableButton;
	}
	
	private void OnDisable()
	{
		gameObject.GetComponent<PressGesture>().Pressed -= Press;
		gameObject.GetComponent<ReleaseGesture>().Released -= Release;	
		lvm.OnStart -= EnableButton;
		lvm.OnFinish -= DisableButton;
	}
	
	void EnableButton() {
		enabled = true;
	}
	
	void DisableButton() {
		enabled = false;
		s_renderer.sprite = s_released;
	}

	public void Press(object sender, EventArgs e) {
		if (enabled) {
						s_renderer.sprite = s_pressed;
						if (OnPressed != null)
								OnPressed ();
				}
	}

	public void Release(object sender, EventArgs e) {
		if (enabled) {
						s_renderer.sprite = s_released;
						if (OnReleased != null)
								OnReleased ();	
				}
	}
}
