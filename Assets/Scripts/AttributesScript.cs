using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributesScript : MonoBehaviour {
    public Canvas nameCanvas;
    public InputField nameInput;
    public Button nameButton;
	void Start()
    {
         displayNameCanvas();
        if (!PlayerPrefs.HasKey("PlayerHighScore")) PlayerPrefs.SetInt("PlayerHighScore", 0);
    }

    public void onClickNameButton()
    {
        if(nameInput.text.Length > 0)
        {
            // Set player name in playerprefs
            PlayerPrefs.SetString("PlayerName", nameInput.text);
            displayNameCanvas();
        }
    }

    void displayNameCanvas()
    {
        CanvasGroup cg = nameCanvas.GetComponent<CanvasGroup>();

        if (!PlayerPrefs.HasKey("PlayerName") || PlayerPrefs.GetString("PlayerName").Length <= 0)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;

        }
        else
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
}
