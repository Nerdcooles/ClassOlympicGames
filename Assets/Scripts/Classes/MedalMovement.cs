﻿using UnityEngine;using System.Collections;/** * Medals animation above the player character */public class MedalMovement : MonoBehaviour{	Vector3 finalPosition;	Vector3 scalePostion;	bool fallDown = false;	bool isUp = false;	void Start ()	{		finalPosition = transform.position + new Vector3 (0f, 100f, 0f);	}	void Update ()	{		if (isUp)			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (2f, 2f, 2f), Time.deltaTime);		else			transform.position = Vector3.Lerp (transform.position, finalPosition, Time.deltaTime);		if (fallDown) {			if ((transform.position.y > finalPosition.y - 20) && !isUp) {				isUp = true;				transform.position = transform.position - new Vector3 (0f, 0f, 2f);				gameObject.AddComponent<Rigidbody2D> ();				rigidbody2D.mass = 50f;				rigidbody2D.gravityScale = 50f;			}		}			}	void OnTriggerEnter2D (Collider2D other)	{		if (other.tag == "Bound") {			Destroy (gameObject);		}	}	public bool FallDown {		get {			return fallDown;		}		set {			fallDown = value;		}	}	public Vector3 FinalPosition {		get {			return finalPosition;		}		set {			finalPosition = value;		}	}}