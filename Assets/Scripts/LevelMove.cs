using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "levelMove")
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
