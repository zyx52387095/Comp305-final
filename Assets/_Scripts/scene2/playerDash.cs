using UnityEngine;
using System.Collections;

public class playerDash : MonoBehaviour {


	public float dashSpeed = 3;
	private bool dashing = false;	
	private float dashTimer;

	
	public Collider2D AttackTrigger;
	
	private Animator anim;
	
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		AttackTrigger.enabled = false;
	}
	
	void Start()
	{
		//player = gameObject.GetComponentInParent<Player> ();
	}
	void Update () {


		
		if (Input.GetKeyDown("s")&& !dashing){

			dashTimer = 5;

			anim.SetBool ("Dashing", dashing);

		}
		if (dashTimer > 0) {

			dashing = true;
			dashTimer--;
		}
		if (dashTimer <= 0) {

			dashing = false;

		}
		
	}
}
