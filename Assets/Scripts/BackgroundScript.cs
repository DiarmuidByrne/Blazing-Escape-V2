using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
	public Transform background;
	public Transform background2;
	public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (background2.position.x <= player.transform.position.x &&
		    (player.transform.position.x - 5) >= background.position.x ) {
			Vector3 temp = background.position;
			temp.x += 79;
			background.position = temp;
		}
		if (background.position.x <= player.transform.position.x &&
		    background2.position.x <= (player.transform.position.x -5)) {
			Vector3 temp2 = background2.position;
			temp2.x += 79;
			background2.position = temp2;
		}
	}
}
