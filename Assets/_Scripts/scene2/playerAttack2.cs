using UnityEngine;
using System.Collections;

public class playerAttack2 : MonoBehaviour {

	private bool attacking = false;
	//private Animator _animator;
	
	private float attackTimer = 0;
	private float attackCD = 0.3f;
	
	public Collider2D AttackTrigger;

	public AudioClip slash;
	
	private Animator anim;
	private AudioSource source;


	
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		source = GetComponent<AudioSource> ();
		AttackTrigger.enabled = false;
	}
	

	void Update () {
		
		if (Input.GetKeyDown("j")&& !attacking){
			
			attacking = true;
			attackTimer= attackCD;
			source.PlayOneShot(slash,1F);
			
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
