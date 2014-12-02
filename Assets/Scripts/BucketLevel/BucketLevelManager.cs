using UnityEngine;
using System.Collections;

public class BucketLevelManager : MonoBehaviour {

	int p1_pts = 0;
	int p2_pts = 0;
	int p3_pts = 0;
	int p4_pts = 0;

	
	public void score(GameManager.ePlayers player) {
		switch(player) {
			case GameManager.ePlayers.p01: p1_pts++; Debug.Log("P1 " + p1_pts); break;
			case GameManager.ePlayers.p02: p2_pts++; Debug.Log("P2 " + p2_pts);  break;
			case GameManager.ePlayers.p03: p3_pts++; Debug.Log("P3 " + p3_pts);  break;
			case GameManager.ePlayers.p04: p4_pts++; Debug.Log("P4 " + p4_pts);  break;		
		}
	}
}
