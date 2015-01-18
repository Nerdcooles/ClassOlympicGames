﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundManager : Singleton<RoundManager> {

	private int round = 0;
	private Image image;

	public void Reset() {
		round = 0;
	}

	public void NextRound() {
		round++;
		image.sprite = Resources.Load <Sprite> ("Sprites/Round/round_" + round);
	}

	public int Round {
		get {
			return round;
		}
	}

	public Image Image {
		get {
			return image;
		}
		set {
			image = value;
		}
	}
}