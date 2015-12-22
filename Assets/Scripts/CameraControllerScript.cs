using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

	public Transform target;
	
	// Update is called once per frame
	void Update () {

		float x = target.transform.position.x;
		x+=2;

		//transform.position = new Vector3 ((x+2f), (0.48f), -10f);
		transform.position = new Vector3 ((x+2f), 4f, -10f);
	}
}
