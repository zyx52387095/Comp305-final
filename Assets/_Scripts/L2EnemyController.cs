using UnityEngine;
using System.Collections;

public class L2EnemyController : MonoBehaviour {
	public GameObject player;
	public float     playerDistance;
	public float     rotationDamping;
	public float     moveSpeed;
	private float 	_posY;

	void Start () {	
		player = GameObject.FindGameObjectWithTag("PlayerL2");
		_posY = 0.0f;
		this._Reset();
	}
	
	
	void Update () {
		playerDistance = Vector2.Distance (transform.position, player.transform.position);
		//Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		//transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*rotationDamping);
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
	}

	private void _Reset()
	{
		//_posY = _player.transform.position.y + Random.Range(180,400);
		_posY = player.transform.position.y + Random.Range(200,250);
		//Vector2 resetPosition = new Vector2(Random.Range(-500, 600), 410);
		Vector2 resetPosition = new Vector2(-180.0f, _posY+800.0f);
		gameObject.GetComponent<Transform>().position = resetPosition;
	}
}
