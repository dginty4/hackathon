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

    bool isCorrect = false;
    bool answered = false;


    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (answered && Input.GetKeyDown(KeyCode.Return)) {
            if (isCorrect) {
                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextScene);
            } else {
                SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
            }
        }
    }

    public void ShowPopup(string message) {
        popupText.text = message;
        popupPanel.GetComponentInChildren<TMP_Text>().text = "Press enter to continue.";
        popupPanel.SetActive(true);
    }

    public void onAnswerSelected(string message, bool correct) {
        answered = true;
        isCorrect = correct;
        if(correct) {
            ShowPopup(message);
        } else {
            ShowPopup(message);
        }
    }
}
