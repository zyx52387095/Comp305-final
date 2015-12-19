using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public GameObject player;
	public float     playerDistance;
	public float     rotationDamping;
	public Text      doorText;

	public int LevelToLoad;

	private gameMaster gm;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<gameMaster>();
	}

	void Update () {
		playerDistance = Vector2.Distance (transform.position, player.transform.position);
		if (playerDistance < 2)
			doorText.text = "Press E";
		else
			doorText.text = "";
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			gm.InputText.text = ("[E] to Enter");
			if (Input.GetKeyDown ("e")) {
				Application.LoadLevel (LevelToLoad);
			}
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
			if(col.CompareTag("Player"))
			{
				if(Input.GetKeyDown("e"))
				{
					Application.LoadLevel(LevelToLoad);
				}
			}
		}

	void OnTriggerExit2D(Collider2D col)
	{
			if(col.CompareTag("Player"))
			{
				gm.InputText.text = (" ");
			}
	}
}