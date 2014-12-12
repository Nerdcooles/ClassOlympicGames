using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	protected GameManager.eLevels level;
	public int num_players = 4;

	protected LevelUI levelUI;

	protected void _start() {
		levelUI = GameObject.Find("LevelUI").GetComponent<LevelUI>() as LevelUI;
		#region DEBUG
		if(GameManager.Instance.getNumPlayer()==0) {
			GameManager.Instance.startMode(GameManager.eGameMode.TRAINING);
			GameManager.Instance.startGame(num_players);
		}
		#endregion
		num_players = GameManager.Instance.getNumPlayer();
	}

	protected IEnumerator GameOver() {
		Debug.Log("GAME OVER");
		levelUI.show(LevelUI.ePanel.Scores);
		yield return new WaitForSeconds(10f);
		if(GameManager.Instance.getGameMode()==GameManager.eGameMode.CLASSIC) {
			levelUI.hide(LevelUI.ePanel.Scores);
			levelUI.show(LevelUI.ePanel.Podium);
			yield return new WaitForSeconds(10f);
			levelUI.hide(LevelUI.ePanel.Podium);
			levelUI.show(LevelUI.ePanel.Medals);
			yield return new WaitForSeconds(10f);
		}
		GameManager.Instance.gameOver(this.level);
	}
}
