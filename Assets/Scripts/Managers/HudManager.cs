using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HudManager : MonoBehaviour {

	public GameObject hud_tablet, hud_phone_4p;

	private const int X_2P = 250;
	void Start () {
		#if UNITY_IPHONE || UNITY_ANDROID
		hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = false;
		hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = false;
		float res = (float)Screen.width/Screen.height;
		if(res > 1.5f && GameManager.Instance.getNumPlayer() == 4) {
			hud_tablet.SetActive(false);
			hud_phone_4p.SetActive(true);
		}else{
			hud_tablet.SetActive(true);
			hud_phone_4p.SetActive(false);
		}
		#else
		hud_tablet.SetActive(true);
		hud_phone_4p.SetActive(false);
		#endif

		GameObject[] button  = hud_tablet.GetComponent<HudKeyboardAdapter>().button;
		float y = button[0].GetComponent<RectTransform>().position.y;
		float width = (float)Screen.width/2f;
		switch(GameManager.Instance.getNumPlayer()) {
		case 1: button[0].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); break;
		case 2: button[0].GetComponent<RectTransform>().position = new Vector3(X_2P, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(X_2P, y, 0f);break;
		case 3: 
			button[1].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); 
			button[2].GetComponent<RectTransform>().position = button[3].GetComponent<RectTransform>().position;break;
		}
	} 
}
