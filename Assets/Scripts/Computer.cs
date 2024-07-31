using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer : MonoBehaviour
{
    public GameObject pressEnterUI;
   
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            pressEnterUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            pressEnterUI.SetActive(false);
        }
    }

    void Update() {
        if (pressEnterUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }        
		if (pressEnterUI.activeSelf && Input.GetKeyDown(KeyCode.P)) {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 2;
            SceneManager.LoadScene(nextScene);
        }
    }
}
