using UnityEngine;
using System.Collections;

public class Rightspike : MonoBehaviour {
	//public GameObject _player;
	private float _posY;
	private PlayerL2Controller myplayer;
	
	// Use this for initialization
	void Start () {
		//this._autoGenerator = GameObject.FindWithTag("AutoGenerator").GetComponent("AutoGenerator") as AutoGenerator;
		//_player = GameObject.FindGameObjectWithTag("Player");
		myplayer = GameObject.FindGameObjectWithTag ("PlayerL2").GetComponent<PlayerL2Controller>();
		this._Reset();	
		_posY = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void _Reset()
	{
		//_posY = _player.transform.position.y + Random.Range(180,400);
		_posY = myplayer.transform.position.y + Random.Range(300,500);
		//Vector2 resetPosition = new Vector2(Random.Range(-500, 600), 410);
		Vector2 resetPosition = new Vector2(200.0f, _posY+1200.0f);
		gameObject.GetComponent<Transform>().position = resetPosition;
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("PlayerL2")) {
			myplayer.damage(15);
			Destroy (gameObject);
		}
		
	}
}
