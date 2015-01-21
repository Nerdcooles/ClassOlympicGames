using UnityEngine;
using System.Collections;

public class MedalMovement : MonoBehaviour {

	Vector3 finalPosition;

	void Start() {
		finalPosition = transform.position + new Vector3 (0f, 90f, 0f);
	}
	void Update() {
		transform.position = Vector3.Lerp (transform.position, finalPosition, Time.deltaTime);

	}
}
