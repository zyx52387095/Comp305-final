using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private bool attacking = false;
	private Animator _animator;

	private float attackTimer = 0;
	private float attackCD = 0.3f;

	public Collider2D AttackTrigger;

	private Animator anim;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		AttackTrigger.enabled = false;
	}


	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("J")&& !attacking){

			attacking = true;
			attackTimer= attackCD;
			this._animator.SetInteger ("AnimState", 3);

		AttackTrigger.enabled = true;
		}
		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				AttackTrigger.enabled = false;
			}
		}
		anim.SetBool ("Attacking", attacking);

	
	}
}
