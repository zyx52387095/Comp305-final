using UnityEngine;
using System.Collections;

public class MorganController : MonoBehaviour {
	PlayerL2Controller player;
	public float     playerDistance;
	public float     rotationDamping;
	public float     moveHSpeed, moveLSpeed;
	private float 	 _posX, _posY;
	public static int number=0;
	
	void Start () {	
		//player = GameObject.FindGameObjectWithTag("PlayerL2");
		player = GameObject.FindGameObjectWithTag ("PlayerL2").GetComponent<PlayerL2Controller>();
		_posX = 0.0f;
		_posY = 0.0f;
		this._Reset();
		number += 1;
	}
	
	
	void Update () {
		playerDistance = Vector2.Distance (transform.position, player.transform.position);
		//Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		//transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*rotationDamping);
		if(playerDistance<300)
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveHSpeed * Time.deltaTime);
		else
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveLSpeed * Time.deltaTime);
	}
	
	private void _Reset()
	{
		//_posY = _player.transform.position.y + Random.Range(180,400);
		if ((number % 2) == 0) {
			_posX = Random.Range (-100, -50);
			_posY = player.transform.position.y + Random.Range (150, 200);
		} else {
			_posX = Random.Range (50, 100);
			_posY = player.transform.position.y + Random.Range (200, 250);
		}
		//Vector2 resetPosition = new Vector2(Random.Range(-500, 600), 410);
		Vector2 resetPosition = new Vector2(_posX, _posY+800.0f);
		gameObject.GetComponent<Transform>().position = resetPosition;
	}


	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("PlayerL2")) {
			if(player.isLive)
				player.damage(10);
			Destroy (gameObject);
		}
		if (otherCollider.gameObject.CompareTag("Leftspike"))
		{
			Destroy (gameObject);
		}
		if (otherCollider.gameObject.CompareTag("Rightspike"))
		{
			Destroy (gameObject);
		}
	}
	
	void OnDestroy() 
	{
		if(player.isLive)
			player.damage(15);
		//Debug.Log("coin was destroyed");
	}
}
