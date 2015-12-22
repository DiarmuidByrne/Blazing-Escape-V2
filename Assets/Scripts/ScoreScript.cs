using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Bitrave.Azure;

public class ScoreScript : MonoBehaviour {
	private Ranking _ranking = new Ranking();
	private AzureMobileServices azure;
	public List<Ranking> _rankingList = new List<Ranking>();

	public Transform character;
	private int bestScore = 0;
	public Text ScoreText;
	private bool newHighScore = false;
	private double charX;
	public Text resultText;
	private int currHighScore;
	public Canvas GameOverCanvas;
	
	private const string AzureEndPoint = "https://blazing-escape.azure-mobile.net/";
	private const string ApplicationKey = "AwnWTrrLcEACaIintQpyouKwtonMQI49";
	private const string FacebookAccessToken = "CAAXY9hlYRyQBACvUuN1hNsETZAPuGEgn0JLElZCigzmYtsbaL3Y4bfotfoc6cFN3ZA4wDzXtUn98jCoygfxDXh0gG7Ies4RVE7544vHJnTRGZANew9veAQe3xaMVaQYipVxwlZBM3ZBPK3BivFbtaEIRJGYBTsZAzSzZBdCzZBhn9WwIaEyV0wXrCRzm4gmMqdU8ZD";

	private string pName;
	private bool dead = false;
	public Transform groundCheck;

	void Start() {
		azure = new AzureMobileServices(AzureEndPoint, ApplicationKey);

		currHighScore = PlayerPrefs.GetInt("PlayerHighScore");
	}

	private void Login() {
		azure.LoginAsync(AuthenticationProvider.Facebook,
						FacebookAccessToken,
						LoginAsyncCallback);
	}

	// Update is called once per frame
	void Update() {
		charX = character.transform.position.x * 0.75;
		if (charX > bestScore) {
			updateScore();
			if (bestScore > currHighScore && newHighScore == false) {
				// Set Score text to different color
				newHighScore = true;
				ScoreText.color = Color.red;
			}
		}

		if (groundCheck.transform.position.y <= -10f && dead == false) {
			Time.timeScale = 0;
			setHighScore();
			setRanking();
			deathMenu();
			dead = true;
		}
	}

	// Updates score when incremented and displays to UI
	private void updateScore() {
		bestScore = (int)charX;
		ScoreText.text = bestScore.ToString() + " m";
		resultText.text = " You ran: " + bestScore.ToString() + " m";
	}

	void deathMenu() {
		CanvasGroup cg = GameOverCanvas.GetComponent<CanvasGroup>();

		cg.alpha = 1;
		cg.blocksRaycasts = true;
		cg.interactable = true;
	}

	void setHighScore() {
		if (bestScore > PlayerPrefs.GetInt("PlayerHighScore")) {
			PlayerPrefs.SetInt("PlayerHighScore", bestScore);
		}
	}

	void setRanking() {
		_ranking.PlayerName = PlayerPrefs.GetString("PlayerName");
		_ranking.PlayerHighScore = PlayerPrefs.GetInt("PlayerHighScore");

		Debug.Log(_ranking.PlayerHighScore + " Name: " + _ranking.PlayerName);

		if (_ranking.PlayerHighScore > 0) {
			// only insert score if greater than 0
			azure.Insert<Ranking>(_ranking);
		}
	}

	private void LoginAsyncCallback(AzureResponse<MobileServiceUser> obj) {
		if (obj.Status == AzureResponseStatus.Success) {
			azure.User = obj.ResponseData;
		} else {
			Debug.Log("Error:" + obj.StatusCode);
		}
	}
}
