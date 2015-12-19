using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudForScene2 : MonoBehaviour {
	
	public Sprite[] healthSprites;
	
	public Image HealthUI;
	
	private Player player;
	
	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}
	
	void Update(){
		
		HealthUI.sprite=healthSprites[player.curHealth];
	}
	
	
	
	
	
}