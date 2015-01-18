using UnityEngine;
using System.Collections;

public class LevelPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	protected GameManager.eColors color;

	protected GameObject button;

	protected LevelManager lvm;
	
	protected Animator animator;
	protected RuntimeAnimatorController animCtrl;

	void Start() {
		lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		lvm.OnFinish += EndPlayer;
		lvm.OnStart += StartPlayer;

		button = GameObject.Find ("UIManager").GetComponent<UIManager> ().getButton (player);
		button.GetComponent<BtnHandler> ().OnPressed += Pressed;
		button.GetComponent<BtnHandler> ().OnReleased += Released;

		if (GameManager.Instance.IsPlaying (player)) {
						color = GameManager.Instance.getColor (player);
						animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString () + "/" + color.ToString () + "_anim");
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
				} else {
						gameObject.SetActive (false);
				}

		Initialize ();
	}

	protected virtual void Initialize() {
	}

	protected virtual void Pressed() {
	}

	protected virtual void Released() {
	}

	protected virtual void StartPlayer() {
	}
	
	protected void EndPlayer() {
				try {
						//IF NOT LAST PLAYER
						int num_players = GameManager.Instance.getNumPlayer ();
						if (num_players == 1 || lvm.getPodium (num_players - 1) != this.player) {
								//IF SINGLE PLAYER OR NOT LAST PLAYER
								animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_winner");
						} else {
								animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
						}
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
			
						GameObject medal = Resources.Load<GameObject> ("Prefabs/Medal_" + lvm.GetPosition (player));
						Instantiate (medal, transform.position + new Vector3 (0f, 90f, 0f), transform.rotation);
				} catch (System.Exception ex) {
						animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Podium/" + color.ToString () + "_podium_loser");
			
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
				}
		}

	public GameManager.eColors Color {
		get {
			return color;
		}
		set {
			color = value;
		}
	}
}
