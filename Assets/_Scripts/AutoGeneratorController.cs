using UnityEngine;
using System.Collections;

public class AutoGeneratorController : MonoBehaviour {
	public GameObject player;
	//public GameObject leftSpike;
	//public GameObject rightSpike;
	//public GameObject coin;
	public Leftspike leftSpike;
	public Rightspike rightSpike;
	public L2CoinController coin;
	public MorganController enemyl2;
	
	
	float prevPosY = 0.0f;
	float currPosY = 0.0f;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("PlayerL2");
	}
	
	// Update is called once per frame
	void Update () {
		currPosY = player.transform.position.y;
		//Debug.Log ("play posY:"+currPosY);
		if( (currPosY-prevPosY)>900 ){
			//Debug.Log ("play posY:"+currPosY);
			prevPosY = currPosY;
			_GenerateSpike();
		}
	}
	
	private void _GenerateSpike()
	{
		int num,i;
		
		Instantiate (leftSpike);
		Instantiate (rightSpike);
		num = Random.Range (1, 2);
		for (i=0; i<num; i++)
			Instantiate (enemyl2);
		
		num = Random.Range (2, 6);
		Debug.Log ("num=" + num);
		for (i=0; i<num; i++)
			Instantiate (coin);
		
	}
}
