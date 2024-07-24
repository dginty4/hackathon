using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasManager : MonoBehaviour
{
    public int secondsAllowed;
    public GameObject screen;
    public GameObject welcome;
    public GameObject go;
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] TextMeshProUGUI secondsLeft;

    // Start is called before the first frame update
    void Start()
    {
        instructions.text = string.Format("You have {0} seconds to reach the computer and answer the question", secondsAllowed);
        StartCoroutine(intro());
        StartCoroutine(countdown());
    }

    IEnumerator intro() {
        Time.timeScale = 0f;

        screen.SetActive(true);
        welcome.SetActive(true);
        instructions.gameObject.SetActive(false);
        go.SetActive(false);

        yield return new WaitForSecondsRealtime(3);
        welcome.SetActive(false);
        instructions.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(3);
        instructions.gameObject.SetActive(false);
        go.SetActive(true);

        yield return new WaitForSecondsRealtime(2);
        screen.SetActive(false);

        Time.timeScale = 1f;
    }

    IEnumerator countdown() {
        for (int i = secondsAllowed; i >= 0; i--) {
            secondsLeft.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Time.timeScale = 0f;
    }
}
