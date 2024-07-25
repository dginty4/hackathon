using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameCanvasManager : MonoBehaviour
{
    public int secondsAllowed;
    public string city;
    public GameObject screen;
    public GameObject welcome;
    public GameObject go;
    public GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] TextMeshProUGUI welcomeText;
    [SerializeField] TextMeshProUGUI secondsLeft;

    private bool gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        instructions.text = string.Format("You have {0} seconds to reach the computer and answer the question", secondsAllowed);
        welcomeText.text = string.Format("Welcome to the {0} office!", city);
        StartCoroutine(Intro());
        StartCoroutine(Countdown());
    }

    void Update() {
        if (gameIsOver) {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.Return)) {
                SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
            }
        }
    }

    IEnumerator Intro() {
        Time.timeScale = 0f;

        // Display only the Welcome message
        screen.SetActive(true);
        welcome.SetActive(true);
        instructions.gameObject.SetActive(false);
        go.SetActive(false);
        gameOverScreen.SetActive(false);

        // Wait for n seconds
        yield return new WaitForSecondsRealtime(3);

        // Display only the instructions 
        welcome.SetActive(false);
        instructions.gameObject.SetActive(true);

        // Wait for n seconds 
        yield return new WaitForSecondsRealtime(3);

        // Dislpay only "Go!" 
        instructions.gameObject.SetActive(false);
        go.SetActive(true);


        // Wait for n seconds
        yield return new WaitForSecondsRealtime(2);

        // Turn off panel
        screen.SetActive(false);

        // Start game 
        Time.timeScale = 1f;
    }

    IEnumerator Countdown() {
        for (int i = secondsAllowed; i >= 0; i--) {
            secondsLeft.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Time.timeScale = 0f;
        gameIsOver = true;
    }
}