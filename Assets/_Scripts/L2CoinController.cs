using UnityEngine;
using System.Collections;

public class L2CoinController : MonoBehaviour {
	//private AutoGeneratorController _autoGenerator;
	private PlayerL2Controller player;
	private float _posX, _posY;
	
	// Use this for initialization
	void Start () {
		//this._autoGenerator = GameObject.FindWithTag("AutoGenerator").GetComponent("AutoGenerator") as AutoGenerator;
		//this._autoGenerator = GameObject.FindGameObjectWithTag ("AutoGenerator").GetComponent<AutoGeneratorController>();
		player = GameObject.FindGameObjectWithTag ("PlayerL2").GetComponent<PlayerL2Controller>();
		this._Reset();
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	void OnCollisionEnter2D(Collision2D otherCollider) {
		if (otherCollider.gameObject.CompareTag ("PlayerL2")) {
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
			player.cur_score += 1;
		Debug.Log("coin was destroyed");
	}
	
	private void _Reset()
	{
		_posX = Random.Range (-200, 195);
		_posY = player.transform.position.y + Random.Range (150, 500);
		//Vector2 resetPosition = new Vector2(Random.Range(-500, 600), 410);
		//Debug.Log ("[Coin] X,Y=" + _posX+' '+_posY);
		Vector2 resetPosition = new Vector2(_posX, _posY);
		gameObject.GetComponent<Transform>().position = resetPosition;
	}
}
