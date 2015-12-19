using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class VelocityRange {
	// PUBLIC INSTANCE VARIABLES
	public float vMin, vMax;
	
	
	
	// CONSTRUCTOR ++++++++++++++++++++++++++++++++
	public VelocityRange(float vMin, float vMax) {
		this.vMin = vMin;
		this.vMax = vMax;
	}
}

public class PlayerL2Controller : MonoBehaviour {
	public float max_Health = 100f;
	public float cur_Health = 0.0f;
	public float max_time = 100f;
	public float cur_time = 0.0f;
	public float max_meters = 100f;
	public float cur_meters = 0.0f;
	public int tot_meters = 0;
	public float max_score = 100;
	public float cur_score = 0.0f;
	
	public float start_meters = 0.0f;
	public float current_meters = 0.0f;
	
	public GameObject healthbar;
	public GameObject timebar;
	public GameObject metersbar;
	public GameObject scorebar;

	
	public bool isLive = true;
	public float diedealy = 100f;
	
	public Text timeLabel;
	public Text metersLabel;
	public Text scoreLabel;
	
	private HighScoreController _highScoreController;
	
	//PUBLIC INSTANCE VARIABLES
	public float speed = 5f;
	public float jump = 10f;
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f);
	
	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
	private AudioSource _jumpSound;
	private AudioSource _bonusSound;
	private AudioSource _punchSound;
	private AudioSource _deathSound;
	private AudioSource _morganSound;
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator;
	
	private float _movingValue = 0;
	private bool _isFacingRight = true;
	private bool _isGrounded = false;
	
	private bool _isLeftwall = false;
	private bool _isRightwall = true;
	
	public bool canDoubleJump;
	
	
	public Text Score;
	public Text lives; 
	public Text gameResult;
	
	//player health
	//public int currentHealth;
	//public int maxHealth=5;
	
	
	
	// Use this for initialization
	void Start () {
		gameResult.text = "";
		cur_Health = max_Health;
		cur_time = max_time;
		cur_meters = 0.0f;
		cur_score = 0.0f;
		InvokeRepeating ("decreasetime", 1f, 1f);
		
		this._rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		this._transform = gameObject.GetComponent<Transform> ();
		this._animator = gameObject.GetComponent<Animator> ();
		
		//this.currentHealth = this.maxHealth;
		
		this.start_meters = this._transform.position.y;
		this.current_meters = this.start_meters;
		
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this._jumpSound = this._audioSources[0];
		this._bonusSound = this._audioSources[1];
		this._punchSound = this._audioSources[2];
		this._deathSound = this._audioSources [3];
		this._morganSound = this._audioSources [4];
		
		this._highScoreController = GameObject.FindWithTag("HighScoreController").GetComponent("HighScoreController") as HighScoreController;
		_highScoreController.HighLevel = 2;
	}
	
	void FixedUpdate () {
		float forceX = 0f;
		float forceY = 0f;
		
		float absVelX = Mathf.Abs (this._rigidbody2D.velocity.x);
		float absVelY = Mathf.Abs (this._rigidbody2D.velocity.y);
		
		current_meters = this._transform.position.y;
		
		this._movingValue = Input.GetAxis ("Horizontal"); // gives moving variable a value of -1 to 1
		
		
		
		
		if (this._movingValue != 0) { // player is moving
			if (isLive){
				//check if player is grounded
				//if (this._isGrounded) {
				if(_isLeftwall||_isRightwall){
					this._animator.SetInteger ("AnimState", 1);
					if (this._movingValue > 0) {
						// move right
						if (absVelX < this.velocityRange.vMax) {
							forceX = this.speed;
							//this._isFacingRight = true;
							//this._flip ();
						}
					} else if (this._movingValue < 0) {
						// move left
						if (absVelX < this.velocityRange.vMax) {
							forceX = -this.speed;
							//this._isFacingRight = false;
							//this._flip ();
						}
					}
				}
			}			
			
		} else {
			// set our idle animation here
			this._animator.SetInteger ("AnimState", 0);
		}
		
		// check if player is jumping
		// double jump not working
		if (Input.GetKey (KeyCode.W)||Input.GetKey (KeyCode.UpArrow))
		{
			if(isLive)
			{
				// chec if player is grounded
				//if (_isGrounded) {
				if(_isLeftwall||_isRightwall){
					this._animator.SetInteger ("AnimState", 2);
					if (absVelY < this.velocityRange.vMax) {
						forceY = this.jump;
						this._isGrounded = false;
						canDoubleJump = true;
					}
				}
				//double jump not working
				else {
					if (canDoubleJump) {
						if (absVelY < this.velocityRange.vMax) {
							forceY = this.jump;
							canDoubleJump = false;
						}
					}
				}
				
			}
			
		}
		
		if (!isLive)
			forceY = -10; 
		
		
		// add force to the player to push him
		this._rigidbody2D.AddForce (new Vector2 (forceX, forceY));
		
		
		
		
		if (Input.GetKey (KeyCode.J)) {
			// chec if player is grounded
			
			this._animator.SetInteger ("AnimState", 3);
		}
		/*
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		if (currentHealth <= 0) {
			Die ();
		}*/
		
		UpdateBars ();
		
		if (cur_Health <= 0)
			Die ();
		
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Leftwall")) {
			this._jumpSound.Play();
		}
		if (otherCollider.gameObject.CompareTag ("Rightwall")) {
			this._jumpSound.Play();
		}
		
		if (otherCollider.gameObject.CompareTag ("Coin")) {
			if(isLive)
				this._bonusSound.Play();
		}
		if (otherCollider.gameObject.CompareTag ("Leftspike")) {
			this._punchSound.Play();
		}
		if (otherCollider.gameObject.CompareTag ("Rightspike")) {
			this._punchSound.Play();
		}
		if (otherCollider.gameObject.CompareTag ("Morgan")) {
			this._morganSound.Play();
		}

	}
	
	void OnCollisionStay2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("Platform")) {
			this._isGrounded =  true;
		}
		
		if (otherCollider.gameObject.CompareTag ("Leftwall")) {
			this._isLeftwall = true;
			this._isRightwall = false;
			//Debug.Log ("Hit leftwall");
			this._flip ();
		}
		
		
		if (otherCollider.gameObject.CompareTag ("Rightwall")) {
			this._isRightwall = true;
			this._isLeftwall = false;
			//Debug.Log("Hit rightwall");
			this._flip ();
		}
		
		
	}
	
	// PRIVATE METHODS
	private void _flip() 
	{
		if (isLive) 
		{
			//if (this._isFacingRight) {
			if (this._isLeftwall) {
				this._transform.localScale = new Vector3 (6f, -6f, 40f); // reset to normal scale
			} else {
				this._transform.localScale = new Vector3 (6f, 6f, 40f);
				//Debug.Log ("_flip2");
			}
		}
	}
	
	//Die function
	void Die(){
		if (isLive)
			_deathSound.Play ();
		isLive = false;
		//this._transform.RotateAround(Vector3.zero, Vector3.right, 20 * Time.deltaTime);
		this._transform.Rotate (0, 0, 5f);
		//restart
		//Application.LoadLevel (Application.loadedLevel);
		
	}
	
	//player damage
	public void damage(int dmg){
		//currentHealth -= dmg;
		
		cur_Health -= dmg;
	}
	//player knockback
	public IEnumerator Knockback(float knockDur, float knockPwr, Vector3 knockDir){
		
		float timer = 0;
		
		while (knockDur>timer) {
			timer += Time.deltaTime;
			_rigidbody2D.AddForce (new Vector3 (knockDir.x * -20, knockDir.y*knockPwr, transform.position.z));
		}
		yield return 0;
		
	}
	
	public void UpdateBars ()
	{
		/*cur_Health -= 2f;*/
		
		
		float calc_Health = cur_Health / max_Health;
		
		//cur_score += 2f;
		float calc_Score = cur_score / max_score;
		
		
		if (cur_Health <= 0)
			cur_Health = 0;
		
		if (cur_score >= max_score)
			cur_score = max_score;
		
		SetHealthBar (calc_Health);
		SetScoreBar (calc_Score);
	}
	
	void decreasetime()
	{
		
		float m;
		float calc_Meters;
		
		cur_time -= 1f;

		float calc_Time = cur_time / max_time;
		
		if (cur_time >= max_time)
			cur_time = max_time;
		SetTimeBar (calc_Time);
		
		
		m = (current_meters - start_meters);
		if(isLive)
			tot_meters = (int)m/20;
		if (m >= 0) {
			m = m % 100;
			cur_meters += m / 200;
			//Debug.Log ("m=" + m/200);
			calc_Meters = cur_meters / max_meters;
			SetMetersBar (calc_Meters);
		}
		if(!isLive)
		{
			if ((this.tot_meters > 999) || (this.cur_score > 29))
				gameResult.text = "PASS";
			else
				gameResult.text = "OVER";
			diedealy -= 20;

			if (diedealy <= 0) {
				//restart
				//Application.LoadLevel (Application.loadedLevel);
				//Application.LoadLevel ("GameOver");
				GotoNextScene();
			}
		}
	}
	
	public void SetHealthBar(float myHealth)
	{
		//myHealth value 0-1, 
		healthbar.transform.localScale = new Vector3(myHealth,healthbar.transform.localScale.y,healthbar.transform.localScale.z);
	}

	void GotoNextScene ()
	{
		if ((this.tot_meters > 999) || (this.cur_score > 29))
			Application.LoadLevel ("Level 3");
		else
			Application.LoadLevel ("GameOver");
	}

	public void SetTimeBar(float myTime)
	{
		//myTime value 0-1, 
		timebar.transform.localScale = new Vector3(myTime,timebar.transform.localScale.y,timebar.transform.localScale.z);
		if (this.cur_time <= 0) 
		{
			GotoNextScene ();
		}
		this.timeLabel.text = " "+this.cur_time;
	}
	
	public void SetMetersBar(float myMeters)
	{
		//myTime value 0-1, 
		//metersbar.transform.localScale = new Vector3(myMeters,metersbar.transform.localScale.y,metersbar.transform.localScale.z);
		this.metersLabel.text = " "+this.tot_meters;
		_highScoreController.HighMeters = this.tot_meters;
	}
	
	public void SetScoreBar(float myScore)
	{
		//myTime value 0-1, 
		//scorebar.transform.localScale = new Vector3(myScore,scorebar.transform.localScale.y,scorebar.transform.localScale.z);
		this.scoreLabel.text = " "+this.cur_score;
		_highScoreController.HighScore = (int)this.cur_score;
	}

}
