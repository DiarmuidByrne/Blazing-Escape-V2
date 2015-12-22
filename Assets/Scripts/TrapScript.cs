using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		GameObject player = GameObject.Find("Character");
		if (coll.gameObject.tag == "Player") {
			player.GetComponent<Collider2D>().enabled = false;
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 650));
			player.transform.FindChild("pointLight2").GetComponent<Light>().range = 20f;
		}
	}
}