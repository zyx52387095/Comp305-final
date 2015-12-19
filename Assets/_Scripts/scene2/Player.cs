using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private int _curTime = 0;

	public Text mission1, mission2;
	public bool isLive = true;

	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
	private AudioSource _jumpSound;
	private AudioSource _bonusSound;
	private AudioSource _punchSound;
	private AudioSource _deathSound;

	public float maxSpeed = 3;
	public float speed = 100f;
	public float jumpPower = 50f;

	public bool grounded;
	public bool canDoubleJump;
	public bool wallSliding;
	public bool dead;
	public bool facingRight = true;

	//Stats
	public int curHealth;
	public int maxHealth=100;

	private Rigidbody2D rb2d;
	private Animator anim;
	private int delay = 2;

	private AudioSource jump;

	public Transform wallCheckPoint;
	public bool wallCheck;
	public LayerMask wallLayermask;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("decreasetime", 1f, 1f);

		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._jumpSound = this._audioSources[0];
		this._punchSound = this._audioSources[1];
		this._deathSound = this._audioSources [2];

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();

		curHealth = maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		anim.SetBool ("Grounded", grounded);
		anim.SetBool ("wallSliding", wallSliding);
		anim.SetBool ("dead", dead);
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

		if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			transform.localScale = new Vector3(-1,1,1);
			facingRight = false;
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) 
		{
			transform.localScale = new Vector3(1,1,1);
			facingRight = true;
		}

		if (Input.GetButtonDown ("Jump") && !wallSliding) 
		{
			this._jumpSound.Play ();
			if(grounded)
			{
				rb2d.AddForce (Vector2.up * jumpPower);
				canDoubleJump = true;
			}
			else
			{
				if(canDoubleJump)
				{

					canDoubleJump = false;
					rb2d.velocity = new Vector2(rb2d.velocity.x,0);
					rb2d.AddForce(Vector2.up* jumpPower);
				}
			}

			

		}

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {

			dead = true;
			this._deathSound.Play();
			Die ();
		}

		if (!grounded) {
		
			wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f,wallLayermask);

			if(facingRight && Input.GetAxis("Horizontal")>0.1f || !facingRight && Input.GetAxis("Horizontal")<0.1f) 
			{
				if(wallCheck)
				{
					HandleWallSliding();
				}
			}

		}
		if (wallCheck == false || grounded) {
		
			wallSliding = false;
		}

	}

	void HandleWallSliding()
	{
		rb2d.velocity = new Vector2 (rb2d.velocity.x, -0.7f);

		wallSliding = true;


		if (Input.GetButtonDown ("Jump")) {



			if(facingRight)
			{
				rb2d.AddForce(new Vector2(-1.5f,1.2f)*jumpPower);
				this.jump.Play();
			}
			else
			{
				rb2d.AddForce(new Vector2(1.5f,1.2f)*jumpPower);
				this.jump.Play();
			}
		}
	}

	void FixedUpdate(){

		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.85f;
	
		float h = Input.GetAxis ("Horizontal");

		if (grounded) {
			rb2d.velocity = easeVelocity;
		}

		if (grounded) {
			rb2d.AddForce ((Vector2.right * speed) * h);
		} else {
			rb2d.AddForce((Vector2.right *speed/2)*h);
		}


		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2(maxSpeed,rb2d.velocity.y);
		}
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}
	//player die
	void Die(){
		//die animation

		isLive = false;
		Destroy(gameObject,delay);

		Application.LoadLevel ("GameOver");

	}
	//player damage
	public void Damage(int dmg){
		curHealth -= dmg;
		gameObject.GetComponent<Animation> ().Play ("player_redflash");
	}
	//knock back
	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir){

		float timer = 0;
		rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
		while (knockDur > timer) {

			timer += Time.deltaTime;

			rb2d.AddForce(new Vector3(knockbackDir.x*-100, knockbackDir.y*knockbackPwr, transform.position.z));
		}

		yield return 0;

	}

	void decreasetime()
	{
		this._curTime++;
		if ((this._curTime % 10) == 4) {
			this.mission1.text="";
			this.mission2.text="";
		}
		else
		{
			this.mission1.text = "Mission";
			this.mission2.text = "> > >Find the exit door.";
		}
	}

}
