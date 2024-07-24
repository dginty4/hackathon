using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenScript : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
