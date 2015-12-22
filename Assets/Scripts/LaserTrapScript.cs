using UnityEngine;
using System.Collections;

public class LaserTrapScript : MonoBehaviour {
	public Transform character;
	public Transform lTrap, lTrapDisabled;
	public Transform startTile;

	private int laserCount;
	private float startPosY;

	// Controls the lasers that follow the player throughout the level.
	void Start () {
		startPosY = startTile.position.y;
		for(int i=0; i<5; i++) {
			GameObject tmpg1 = Instantiate(Resources.Load("laser_trap", typeof(GameObject))) as GameObject;
			tmpg1.transform.parent = lTrapDisabled;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(Transform child in lTrapDisabled.transform) {
			child.position = new Vector2((character.position.x - 30f), (startPosY+5));
		}

		if(lTrap.childCount == 0) {
			// Amount of lasers currently active
			laserCount = Random.Range(1, 2);
			activateLasers(laserCount);
		}

		if(lTrap.GetChild(0).position.x < (character.position.x+40f)) {
			// Keep moving laser to the right until offscreen
			lTrap.GetChild(0).position = new Vector2(lTrap.GetChild(0).position.x + .2f, lTrap.GetChild(0).position.y);
		} else {
			lTrap.GetChild(0).SetParent(lTrapDisabled);
		}
	}

	void activateLasers(int count) {
		// If no lasers active, add to active parent
		for (int i = 0; i < count; i++) {

			lTrapDisabled.GetChild(i).SetParent(lTrap);
		}
	}
}
