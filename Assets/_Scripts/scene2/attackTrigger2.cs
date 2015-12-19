using UnityEngine;
using System.Collections;

public class attackTrigger2 : MonoBehaviour {

	public int dmg =20;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag("Enemy")){
			col.SendMessageUpwards("Damage",dmg);
		}
	}
	

}
