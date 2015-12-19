using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
	private HighScoreController _highScoreController;
	public Text HighLevelLabel;
	public Text HighMetersLabel;
	public Text HighScoreLabel;
	
	// Use this for initialization
	void Start () {
		this._highScoreController = GameObject.FindWithTag("HighScoreController").GetComponent("HighScoreController") as HighScoreController;	
		this.HighLevelLabel.text = "Level " + this._highScoreController.HighLevel;
		this.HighMetersLabel.text = "Meters: " + this._highScoreController.HighMeters;
		this.HighScoreLabel.text = "Score: " + this._highScoreController.HighScore;
	}
	
	// Update is called once per frame
	void Update () {
		if (this._highScoreController.HighLevel == 2) {
			this.HighLevelLabel.text = "Level " + this._highScoreController.HighLevel;
			this.HighMetersLabel.text = "Meters: " + this._highScoreController.HighMeters;
			this.HighScoreLabel.text = "Score: " + this._highScoreController.HighScore;
		} else {
			this.HighLevelLabel.text = "";
			this.HighMetersLabel.text = "";
			this.HighScoreLabel.text ="";
		}
	}
	
	public void OnLevel1ButtonClick()
	{   this._highScoreController.HighLevel = 1;
		Application.LoadLevel ("TutorialLevel");
	}
	
	public void OnLevel2ButtonClick()
	{
		this._highScoreController.HighLevel = 2;
		Application.LoadLevel ("Level2");
	}
	
	public void OnLevel3ButtonClick()
	{
		this._highScoreController.HighLevel = 3;
		Application.LoadLevel ("Level 3");
	}
}
