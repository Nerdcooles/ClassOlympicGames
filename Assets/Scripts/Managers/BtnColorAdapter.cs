using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnColorAdapter : MonoBehaviour {
			
	public GameManager.ePlayers player;
		
	void Awake() {
		try {
			GameManager.eColors color = GameManager.Instance.getColor (player);
			gameObject.GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Buttons/" + color.ToString () + "_" + player.ToString ());
			SpriteState sprite_pressed = new SpriteState();
			sprite_pressed.pressedSprite = Resources.Load <Sprite> ("Sprites/Buttons/" + color.ToString () + "_" + player.ToString () + "_pressed");
			gameObject.GetComponent<Button> ().spriteState = sprite_pressed;
		} catch {
			gameObject.SetActive (false);
		}
	}
}
