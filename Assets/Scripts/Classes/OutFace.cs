﻿using UnityEngine;
using System.Collections;

public class OutFace : MonoBehaviour {

	public GameManager.ePlayers player;
	GameObject character;

	SpriteRenderer spriteRenderer;
	float sceneWidth;

	void Start () {
		if (GameManager.Instance.IsPlaying (player)) {
			character = GameObject.Find (player.ToString());
			string color = character.GetComponent<LevelPlayer>().Color.ToString();
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Characters/" + color + "/" + color + "_out");
			sceneWidth = GameObject.Find("UIManager").GetComponent<UIManager>().SceneWidth;
			transform.position += new Vector3(sceneWidth + 50f, character.transform.position.y, 0f);
			spriteRenderer.enabled = false;
		}else{
			gameObject.SetActive(false);
		}
	}

	void Update() {
		if(Camera.main.transform.position.x - character.transform.position.x > -sceneWidth + 100) {
			spriteRenderer.enabled = true;
		}
		else {
			spriteRenderer.enabled = false;
		}
	}
}