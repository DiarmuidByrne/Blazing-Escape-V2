using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelCreator : MonoBehaviour {
    public GameObject character;
	public GameObject startTile;
	public GameObject gLeft, gMiddle, gRight, gBlank, sTrap;

	// Private Variables
	private float startUpPosY;
	private const float TILE_WIDTH = 2.4f;
	private const float RIGHT_TILE_WIDTH = 1.1f;
	private GameObject tmpTile;

	private GameObject collectedTiles;
	private GameObject gameLayer;
	private GameObject player;
    public Canvas gameOverCanvas;
    private CanvasGroup cgGameOver;

	private int blankCounter = 0;
    private int trapCounter = 0;
	private int middleCounter = 0;
	private int middleTotal = 0;
	private string lastTile = "right";

	private GameObject newEnemy;

	// Use this for initialization
	void Start () {
		// Make game over screen invisible on start
		//player.GetComponent<Collider2D>().enabled = true;
		Time.timeScale = 1;
		cgGameOver = gameOverCanvas.GetComponent<CanvasGroup>();
        cgGameOver.alpha = 0;
        cgGameOver.blocksRaycasts = false;
        cgGameOver.interactable = false;

        gameLayer = GameObject.Find ("GameLayer");
		collectedTiles = GameObject.Find ("tiles");
		player = GameObject.Find ("Character");

		for (int i = 0; i<30; i++) {
			GameObject tmpg1 = Instantiate (Resources.Load ("ground_left", typeof(GameObject))) as GameObject;
			tmpg1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			GameObject tmpg2 = Instantiate (Resources.Load ("ground_middle", typeof(GameObject))) as GameObject;
			tmpg2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			GameObject tmpg3 = Instantiate (Resources.Load ("ground_right", typeof(GameObject))) as GameObject;
			tmpg3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			GameObject tmpg4 = Instantiate (Resources.Load ("ground_blank", typeof(GameObject))) as GameObject;
			tmpg4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
		}

		for (int i = 0; i<25; i++) {
			GameObject tmpg5 = Instantiate (Resources.Load ("spike_trap", typeof(GameObject))) as GameObject;
			tmpg5.transform.parent = collectedTiles.transform.FindChild ("sTrap").transform;
			tmpg5.transform.position = Vector2.zero;

		}

		collectedTiles.transform.position = new Vector2(-60.0f, 0f);
		startUpPosY = startTile.transform.position.y;
		FillScene ();

	}

	void FixedUpdate(){
		foreach (Transform child in gameLayer.transform) {
			if (child.position.x < (player.transform.position.x - 20f)) {
				switch (child.gameObject.name) {
				case "ground_left(Clone)":
					child.gameObject.transform.position = gLeft.transform.position;
					child.gameObject.transform.parent = gLeft.transform;
					break;
				case "ground_middle(Clone)":
					child.gameObject.transform.position = gMiddle.transform.position;
					child.gameObject.transform.parent = gMiddle.transform;									
					break;
				case "ground_right(Clone)":
					child.gameObject.transform.position = gRight.transform.position;
					child.gameObject.transform.parent = gRight.transform;
					break;
				case "ground_blank(Clone)":
					child.gameObject.transform.position = gBlank.transform.position;
					child.gameObject.transform.parent = gBlank.transform;
					break;
				case "sTrap(Clone)":
					child.gameObject.transform.position = sTrap.transform.position;
					child.gameObject.transform.parent = sTrap.transform;
					break;
				default:
					Destroy (child.gameObject);
					break;
				}
			}
		}

		if (gameLayer.transform.childCount < 25) {
			SpawnTile();
		}
	}

	void SpawnTile() {
		if (blankCounter > 0) {
			SetTile("blank");
			blankCounter--;
			return;
		}
		if (middleCounter > 0) { 
            if(trapCounter > 0 && middleCounter < (middleTotal-2)) {
                randomizeEnemy();
                trapCounter--;
            }
			SetTile("middle");
			middleCounter--;
			return;
		}

		if (lastTile == "blank") {
			SetTile ("left");
			middleTotal = (int)Random.Range (3, 15);
			middleCounter = middleTotal;
            trapCounter = (int)Random.Range(0, 3);
		} else if (lastTile == "right") {
			blankCounter = (int)Random.Range (5, 7); 
		} else if (lastTile == "middle") {
			SetTile ("right");
		}
	}

	private void FillScene() {

		var rand = Random.Range (15, 25);
		for (int i =0; i < rand; i++) {
			SetTile("middle");

		}
		SetTile ("right");
	}

	private void randomizeEnemy() {
        // Adds enemy to random middle tiles
		newEnemy = sTrap.transform.GetChild(0).gameObject;
		newEnemy.transform.parent = gameLayer.transform;

        //newEnemy.transform.position = new Vector2(startTile.transform.position.x+TILE_WIDTH, startUpPosY + (startUpPosY*TILE_WIDTH +(TILE_WIDTH*2)));
        newEnemy.transform.position = new Vector2(startTile.transform.position.x + TILE_WIDTH, startUpPosY + TILE_WIDTH);
	}

	public void SetTile(string type){
		switch (type) {
		case "left":
			tmpTile = gLeft.transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = gMiddle.transform.GetChild(0).gameObject;	
			break;
		case "right":
			tmpTile = gRight.transform.GetChild (0).gameObject;	
			break;
		case "blank":
			tmpTile = gBlank.transform.GetChild (0).gameObject;	
			break;
		}

		tmpTile.transform.parent = gameLayer.transform;
        tmpTile.transform.position = new Vector3(startTile.transform.position.x + (TILE_WIDTH), startUpPosY, startTile.transform.position.z);

		startTile = tmpTile;
		lastTile = type;
	}
}
