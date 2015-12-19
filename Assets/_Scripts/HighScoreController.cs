using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour {
	public int HighLevel;
	public int HighMeters;
	public int HighScore;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
