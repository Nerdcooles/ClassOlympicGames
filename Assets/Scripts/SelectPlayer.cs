using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectPlayer : MonoBehaviour {

	public GameObject blue_btn;
	public GameObject red_btn;
	public GameObject green_btn;
	public GameObject yellow_btn;

	public GameObject blue;
	public GameObject red;
	public GameObject green;
	public GameObject yellow;

	public GameObject back_btn;
	
	private int pos;
	private bool blue_tapped;
	private bool red_tapped;
	private bool green_tapped;
	private bool yellow_tapped;
	
	void Awake() {
		pos = 0;
		blue_tapped = false;
		red_tapped = false;
		green_tapped = false;
		yellow_tapped = false;
		blue.SetActive(false);
		red.SetActive(false);
		green.SetActive(false);
		yellow.SetActive(false);
	}

	private void OnEnable()
	{
		blue_btn.GetComponent<TapGesture>().Tapped += BlueTap;
		red_btn.GetComponent<TapGesture>().Tapped += RedTap;
		green_btn.GetComponent<TapGesture>().Tapped += GreenTap;
		yellow_btn.GetComponent<TapGesture>().Tapped += YellowTap;
		

		back_btn.GetComponent<TapGesture>().Tapped += Back;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			blue_btn.GetComponent<TapGesture>().Tapped -= BlueTap;
			red_btn.GetComponent<TapGesture>().Tapped -= RedTap;
			green_btn.GetComponent<TapGesture>().Tapped -= GreenTap;
			yellow_btn.GetComponent<TapGesture>().Tapped -= YellowTap;

			back_btn.GetComponent<TapGesture>().Tapped -= Back;
		}catch{}
	}
	
	private void BlueTap(object sender, EventArgs e)
	{
		if(!blue_tapped) {
			blue_tapped = true;
			pos++;
			blue.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/blue_p0" + pos);
			blue.SetActive(true);
			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.blue);
			CheckNumber();
		}
	}
	
	private void RedTap(object sender, EventArgs e)
	{
		if(!red_tapped) {
			red_tapped = true;
			pos++;
			red.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/red_p0" + pos);
			red.SetActive(true);
			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.red);
			CheckNumber();
		}
	}
	
	private void GreenTap(object sender, EventArgs e)
	{
		if(!green_tapped) {
			green_tapped = true;
			pos++;
			green.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/green_p0" + pos);
			green.SetActive(true);
			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.green);
			CheckNumber();
		}
	}
	
	private void YellowTap(object sender, EventArgs e)
	{
		if(!yellow_tapped) {
			yellow_tapped = true;
			pos++;
			yellow.GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> ("Sprites/Buttons/yellow_p0" + pos);
			yellow.SetActive(true);
			GameManager.Instance.setColor((GameManager.ePlayers)(pos-1), GameManager.eColors.yellow);
			CheckNumber();
		}
	}
	
	private void CheckNumber() {
		if(pos == GameManager.Instance.getNumPlayer())
			MenuManager.startGame();
	}
	
	void Back (object sender, EventArgs e)
	{
		MenuManager.selectNumber();
		
		Debug.Log("BACK TO SELECT NUMBER");
	}
}
