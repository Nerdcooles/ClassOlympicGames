using UnityEngine;
using System.Collections;

public class MusicCtrl : MonoBehaviour {

	void Awake() {
		if (MusicManager.Instance.Source.clip != MusicManager.Instance.MenuClip) {
			MusicManager.Instance.Source.Stop();
			MusicManager.Instance.Source.clip = MusicManager.Instance.MenuClip;
			MusicManager.Instance.Source.Play();
				}

	}
}
