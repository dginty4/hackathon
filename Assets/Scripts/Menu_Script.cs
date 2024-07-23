using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("BrianSandbox", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}