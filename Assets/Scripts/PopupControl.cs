using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PopupControl : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupText;

    public Button popupButton;


    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup(string message, string buttonMessage, bool isCorrect) {
        popupText.text = message;
        popupPanel.GetComponentInChildren<TMP_Text>().text = buttonMessage;
        popupPanel.SetActive(true);

        if(!isCorrect) {
             SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        } else {
            int nextScene =SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(nextScene);
        }
    }

    public void onAnswerSelected(string message, bool isCorrect) {
        if(isCorrect) {
            ShowPopup(message, "Next Level", isCorrect);
        } else {
            ShowPopup(message, "Home", isCorrect);
        }
    }
}
