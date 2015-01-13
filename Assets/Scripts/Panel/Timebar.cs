using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timebar : MonoBehaviour {

	private const float MIN = -873;
	private const float MAX = 0;

	public GameObject full_bar;
	private RectTransform pos;
	private float y, z;
	private float size;

	void Start () {
		size = MAX - MIN;
		pos = full_bar.GetComponent<RectTransform> ();
		y = pos.position.y;
		z = pos.position.z;
		pos.position = new Vector3(MIN, y, z);
	}

	public void setPercentage(float percentage) {
		if(percentage < 0 || percentage > 1)
			throw new System.ArgumentException("percentage must be between 0 and 1", "percentage");
		pos.position = new Vector3 (MIN + (percentage * size), y, z);
	}
}
