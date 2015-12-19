using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

	private PlayerController player;

	void Start(){

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D col) {

		if(col.CompareTag("Player")) 
		{
			player.damage(1);

			StartCoroutine(player.Knockback(0.02f,-10,player.transform.position));

		}
	
	}

}
