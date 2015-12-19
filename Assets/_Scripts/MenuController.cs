using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	//Public Variables
	public RectTransform panel; //To hold the ScrollPanel;
	public Button[] bttn;
	public RectTransform center;
	
	//Private Variables
	private HighScoreController _highScoreController;
	private float[] distance;
	private bool dragging = false;
	private int bttnDistance;
	private int minButtonNum;
	
	// Use this for initialization
	void Start () {
		this._highScoreController = GameObject.FindWithTag("HighScoreController").GetComponent("HighScoreController") as HighScoreController;	
		int bttnLength = bttn.Length;
		distance = new float[bttnLength];
		
		bttnDistance = (int)Mathf.Abs (bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
		print (bttnDistance);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<bttn.Length; i++) {
			distance[i] = Mathf.Abs (center.transform.position.x - bttn[i].transform.position.x);
		}
	}
	
	public void OnLevel1ButtonClick()
	{
		this._highScoreController.HighLevel = 1;
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
