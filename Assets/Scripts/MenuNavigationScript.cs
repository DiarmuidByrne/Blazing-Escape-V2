using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Bitrave.Azure;

public class MenuNavigationScript : MonoBehaviour {
	public Canvas pauseScreen;

	public void LoadMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MenuScene");
    }
    
    public void LoadGame()
    {
        Time.timeScale = 1;
		Application.LoadLevel("GameScene");
    }

	public void Pause() {
		Time.timeScale = 0;
		CanvasGroup cvg = pauseScreen.GetComponent<CanvasGroup>();
		cvg.alpha = 1;
		cvg.blocksRaycasts = true;
		cvg.interactable = true;
	}

	public void Resume() {
		Time.timeScale = 1;
		CanvasGroup cvg = pauseScreen.GetComponent<CanvasGroup>();
		cvg.alpha = 0;
		cvg.blocksRaycasts = false;
		cvg.interactable = false;
	}

	public void LoadLeaderBoards() {
		Time.timeScale = 1;
		Application.LoadLevel("Leaderboards");
	}

}
