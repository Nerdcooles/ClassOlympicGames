using UnityEngine;
using System.Collections;

public class ArcheryTeacher : MonoBehaviour {

	private float moveSpeed;	
	private Vector3 moveDirection;

	void Start () {
		moveSpeed = 2f;
		moveDirection = Vector3.right;
	}
	
	void Update () {
		// 1
		Vector3 currentPosition = transform.position;
		if(currentPosition.x > 5.0f || currentPosition.x < -5.0f)
			moveDirection = moveDirection * -1;
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
	}
}
