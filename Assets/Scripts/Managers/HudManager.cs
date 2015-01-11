using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HudManager : MonoBehaviour {

	public GameObject hud_tablet, hud_phone_4p;

	private const int X_2P = 250;
	bool isPhone4p = false;

	void Awake () {
		#if UNITY_IPHONE || UNITY_ANDROID
			#if UNITY_EDITOR
			hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = true;
			hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = true;
			#else	
			hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = false;
			hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = false;
		#endif
		float res = (float)Screen.width/Screen.height;
		if(res > 1.5f && GameManager.Instance.getNumPlayer() == 4) {
			isPhone4p = true;
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

		#if UNITY_EDITOR
				hud_tablet.GetComponent<HudKeyboardAdapter> ().enabled = true;
				hud_phone_4p.GetComponent<HudKeyboardAdapter> ().enabled = true;
		#endif

		GameObject[] button  = hud_tablet.GetComponent<HudKeyboardAdapter>().button;
		float y = button[0].GetComponent<RectTransform>().position.y;
		float width = (float)Screen.width/2f;
		switch(GameManager.Instance.getNumPlayer()) {
		case 1: button[0].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); break;
		case 2: button[0].GetComponent<RectTransform>().position = new Vector3(-X_2P, y, 0f);  
			button[1].GetComponent<RectTransform>().position = new Vector3(X_2P, y, 0f);break;
		case 3: 
			button[1].GetComponent<RectTransform>().position = new Vector3(0f, y, 0f); 
			button[2].GetComponent<RectTransform>().position = button[3].GetComponent<RectTransform>().position;break;
		}
	} 

	public GameObject getButton(GameManager.ePlayers player) {
		GameObject[] button;
		if (isPhone4p) {
			button  = hud_phone_4p.GetComponent<HudKeyboardAdapter>().button;
				} else {
			button  = hud_tablet.GetComponent<HudKeyboardAdapter>().button;
				}
		return button[player.GetHashCode()];
	}
}
