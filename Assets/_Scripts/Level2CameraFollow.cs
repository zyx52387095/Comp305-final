using UnityEngine;
using System.Collections;

public class Level2CameraFollow : MonoBehaviour {

	private Vector2 velocity;
	public float smoothTimeY;
	public float smoothTimeX;
	float posX = 0.0f;
	float posY = 0.0f;
	private float _posY;
	
	public GameObject playerl2;
	// Use this for initialization
	void Start () {
		playerl2 = GameObject.FindGameObjectWithTag("PlayerL2");
		//posX = Mathf.SmoothDamp (transform.position.x, playerl2.transform.position.x, ref velocity.x, smoothTimeX);
		//posX = 258.0f;
	}
	
	void Update(){
		
		_posY = Random.Range(150,225);
		
		//posX = Mathf.SmoothDamp (transform.position.x, playerl2.transform.position.x, ref velocity.x, smoothTimeX);
		posY = Mathf.SmoothDamp (transform.position.y, playerl2.transform.position.y+_posY, ref velocity.y, smoothTimeY + 0.3f);
		
		//transform.position = new Vector3 (posX, posY, transform.position.z);
		transform.position = new Vector3 (-0.3f, posY, transform.position.z);
	}
}
