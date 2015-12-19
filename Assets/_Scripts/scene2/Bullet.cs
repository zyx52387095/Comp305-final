using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int curHealth;
	public int maxHealth=20;
	public AudioSource hit;

	void Start () {


		curHealth = maxHealth;
		
		
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {

			if (col.CompareTag ("Player")) {
				col.GetComponent<Player> ().Damage (1);

			}
			this.hit.Play();
			Destroy (gameObject);

		}
	}

	void Update (){

		if (curHealth <= 0) {
			
			Destroy (gameObject);
		}
	}
		
	public void Damage(int damage)
	{
		curHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("player_redflash");
	}


}

