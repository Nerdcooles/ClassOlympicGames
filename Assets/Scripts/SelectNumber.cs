using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectNumber : MonoBehaviour {

	public GameObject p1_btn;
	public Sprite p1_btn_released;
	public Sprite p1_btn_pressed;
	public Sprite p1_btn_disabled;

	public GameObject p2_btn;
	public Sprite p2_btn_released;
	public Sprite p2_btn_pressed;

	public GameObject p3_btn;
	public Sprite p3_btn_released;
	public Sprite p3_btn_pressed;

	public GameObject p4_btn;
	public Sprite p4_btn_released;
	public Sprite p4_btn_pressed;

	public GameObject back_btn;

	private bool isClassic;
	
	
	void Awake() {
		if(GameManager.Instance.getGameMode() == GameManager.eGameMode.CLASSIC) {
			Debug.Log("p1 disabled");
			isClassic = false;
			p1_btn.GetComponent<SpriteRenderer>().sprite = p1_btn_disabled;
		}else{
			Debug.Log("p1 enabled");
			isClassic = true;
			p1_btn.GetComponent<SpriteRenderer>().sprite = p1_btn_released;
		}
		p2_btn.GetComponent<SpriteRenderer>().sprite = p2_btn_released;
		p3_btn.GetComponent<SpriteRenderer>().sprite = p3_btn_released;
		p4_btn.GetComponent<SpriteRenderer>().sprite = p4_btn_released;
	}

	private void OnEnable()
	{
		p1_btn.GetComponent<PressGesture>().Pressed += press1;
		p1_btn.GetComponent<ReleaseGesture>().Released += release1;
		p2_btn.GetComponent<PressGesture>().Pressed += press2;
		p2_btn.GetComponent<ReleaseGesture>().Released += release2;
		p3_btn.GetComponent<PressGesture>().Pressed += press3;
		p3_btn.GetComponent<ReleaseGesture>().Released += release3;
		p4_btn.GetComponent<PressGesture>().Pressed += press4;
		p4_btn.GetComponent<ReleaseGesture>().Released += release4;

		back_btn.GetComponent<TapGesture>().Tapped += Back;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			p1_btn.GetComponent<PressGesture>().Pressed -= press1;
			p1_btn.GetComponent<ReleaseGesture>().Released -= release1;
			p2_btn.GetComponent<PressGesture>().Pressed -= press2;
			p2_btn.GetComponent<ReleaseGesture>().Released -= release2;
			p3_btn.GetComponent<PressGesture>().Pressed -= press3;
			p3_btn.GetComponent<ReleaseGesture>().Released -= release3;
			p4_btn.GetComponent<PressGesture>().Pressed -= press4;
			p4_btn.GetComponent<ReleaseGesture>().Released -= release4;

			back_btn.GetComponent<TapGesture>().Tapped -= Back;
		}catch{}
	}
	
	private void press1(object sender, EventArgs e)
	{
		if(isClassic)
			p1_btn.GetComponent<SpriteRenderer>().sprite = p1_btn_pressed;
	}

	private void release1(object sender, EventArgs e)
	{
		if(isClassic) {
			Debug.Log("START 1 PLAYER");
			MenuManager.selectPlayer(1);
		}
	}	
	
	private void press2(object sender, EventArgs e)
	{
		p2_btn.GetComponent<SpriteRenderer>().sprite = p2_btn_pressed;
	}
	
	private void release2(object sender, EventArgs e)
	{
			Debug.Log("START 2 PLAYER");
		MenuManager.selectPlayer(2);
	}	
	
	private void press3(object sender, EventArgs e)
	{
		p3_btn.GetComponent<SpriteRenderer>().sprite = p3_btn_pressed;
	}
	
	private void release3(object sender, EventArgs e)
	{
		Debug.Log("START 3 PLAYER");
		MenuManager.selectPlayer(3);
	}	
	
	private void press4(object sender, EventArgs e)
	{
		p4_btn.GetComponent<SpriteRenderer>().sprite = p4_btn_pressed;
	}
	
	private void release4(object sender, EventArgs e)
	{
		Debug.Log("START 4 PLAYER");
		MenuManager.selectPlayer(4);
	}	
	
	
	void Back (object sender, EventArgs e)
	{
		MenuManager.selectMode();
		
		Debug.Log("BACK TO SELECT MODE");
	}
}
