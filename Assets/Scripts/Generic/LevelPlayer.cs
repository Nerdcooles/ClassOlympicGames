using UnityEngine;
using System.Collections;

public class LevelPlayer : MonoBehaviour {

	public GameManager.ePlayers player;
	protected GameManager.eColors color;
	protected bool finished;

	protected GameObject button;

	protected LevelManager lvm;
	
	protected Animator animator;
	protected RuntimeAnimatorController animCtrl;

	void Start() {

		if (GameManager.Instance.IsPlaying (player)) {
						lvm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
						lvm.OnFinish += EndPlayer;
						lvm.OnStart += StartPlayer;
						lvm.OnShowMedals += ShowMedal;
						
						button = GameObject.Find ("btn_" + player.ToString());
						button.GetComponent<BtnHandler> ().OnPressed += Pressed;
						button.GetComponent<BtnHandler> ().OnReleased += Released;

						lvm.OnPause += button.GetComponent<BtnHandler>().DisableButton;
						lvm.OnResume += button.GetComponent<BtnHandler>().EnableButton;

						color = GameManager.Instance.getColor (player);
						animCtrl = Resources.Load <RuntimeAnimatorController> ("Sprites/Characters/" + color.ToString () + "/" + color.ToString () + "_anim");
						animator = GetComponent<Animator> ();			
						animator.runtimeAnimatorController = animCtrl;
						finished = false;
						Initialize ();
				}
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
		finished = true;
		}

	protected void ShowMedal() {
		try {
			//IF NOT LAST PLAYER
			int num_players = GameManager.Instance.getNumPlayer ();
			if (num_players == 1 || lvm.getPodium (num_players - 1) != this.player) {
				//IF SINGLE PLAYER OR NOT LAST PLAYER
				animator.SetBool("isWinner", true);
			} else {
				animator.SetBool("isLoser", true);
			}
			
			GameObject medal = Resources.Load<GameObject> ("Prefabs/Medal_" + lvm.GetPosition (player));
			medal.GetComponent<MedalMovement>().enabled = true;
			Instantiate (medal, transform.position + new Vector3 (0f, 0f, 1f), transform.rotation);
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
