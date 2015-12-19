using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public Sprite[] healthSprites;

	public Image HealthUI;

	private PlayerController player;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}

	void Update(){

		HealthUI.sprite=healthSprites[player.currentHealth];
	}





}
