using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float velocity = 1f;

	public Transform sightStart;
	public Transform sightEnd;

	public int curHealth;
	public int maxHealth=35;

	public LayerMask detectWhat;

	public bool colliding;
	


	// Use this for initialization
	void Start () {
	
		curHealth = maxHealth;


	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D>().velocity = new Vector2 (velocity, GetComponent<Rigidbody2D>().velocity.y);

		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position,detectWhat);

		if (colliding) {

			transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
			velocity*=-1;
		}

		if (curHealth <= 0) {
			
			Destroy(gameObject);
		}
	
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(sightStart.position, sightEnd.position);
	}

	public void Damage(int damage)
	{
		curHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("player_redflash");
	}

}
