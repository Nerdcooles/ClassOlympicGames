using UnityEngine;
using System;
using TouchScript.Gestures;


public class SelectLevel : MonoBehaviour {

	public GameObject bus;
	public GameObject bucket;
	public GameObject archery;

	private void OnEnable()
	{
		bus.GetComponent<TapGesture>().Tapped += startBus;
		bucket.GetComponent<TapGesture>().Tapped += startBucket;
		archery.GetComponent<TapGesture>().Tapped += startArchery;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		try{
			bus.GetComponent<TapGesture>().Tapped -= startBus;
			bucket.GetComponent<TapGesture>().Tapped -= startBucket;
			archery.GetComponent<TapGesture>().Tapped -= startArchery;
		}catch{}
	}
	
	private void startBus(object sender, EventArgs e)
	{
		Debug.Log("TRAINING BUS");
		GameManager.Instance.startLevel(GameManager.eLevels.Bus);
	}
	
	private void startBucket(object sender, EventArgs e)
	{
		Debug.Log("TRAINING BUCKET");
		GameManager.Instance.startLevel(GameManager.eLevels.Bucket);
	}
	
	private void startArchery(object sender, EventArgs e)
	{
		Debug.Log("TRAINING ARCHERY");
		GameManager.Instance.startLevel(GameManager.eLevels.Archery);
	}
}
