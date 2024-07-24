using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using TMPro;

// Class representing the response from the OpenTDB API
[System.Serializable]
public class OpenTDBResponse
{
    public int response_code;
    public List<Question> results;
}

// Class representing each quiz question
[System.Serializable]
public class Question
{
    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_answer;
    public List<string> incorrect_answers;
}

public class QuizManager : MonoBehaviour
{
    private string apiUrl = "https://opentdb.com/api.php?amount=1&category=18&difficulty=easy&type=multiple";

    public Text questionText;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button option4Button;
    public Text feedbackText;

    private string correctAnswer;

    public PopupControl popupControl;

    void Start()
    {
        // Log to check if all UI elements are assigned
        Debug.Log("Checking UI elements assignment...");
        if (questionText == null || option1Button == null || option2Button == null || option3Button == null || option4Button == null || feedbackText == null)
        {
            Debug.LogError("One or more UI elements are not assigned in the Inspector.");
            return;
        }
        Debug.Log("All UI elements are assigned. Starting coroutine to fetch quiz question...");
        StartCoroutine(GetQuizQuestion());
    }

    IEnumerator GetQuizQuestion()
    {
        Debug.Log("Sending request to: " + apiUrl);
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        Debug.LogError("responseCode: " + request.responseCode);

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError("responseCode: " + request.responseCode);

            if(request.responseCode == 429) {
                System.Threading.Thread.Sleep(50);
                StartCoroutine(GetQuizQuestion());
            } else {
                Debug.LogError("Error: " + request.error);
                questionText.text = "Error fetching question.";
                feedbackText.text = "";
                yield break;
            }    
        }

        string jsonResponse = request.downloadHandler.text;
        Debug.Log("Response received: " + jsonResponse);
        OpenTDBResponse quizResponse = JsonUtility.FromJson<OpenTDBResponse>(jsonResponse);

        if (quizResponse.results.Count > 0)
        {
            Question question = quizResponse.results[0];
            questionText.text = question.question;
            correctAnswer = question.correct_answer;

            List<string> options = new List<string>(question.incorrect_answers);
            options.Add(correctAnswer);
            options = options.OrderBy(x => Random.value).ToList();

            if (options.Count == 4) {
                option1Button.GetComponentInChildren<TMP_Text>().text = options[0];
                option2Button.GetComponentInChildren<TMP_Text>().text = options[1];
                option3Button.GetComponentInChildren<TMP_Text>().text = options[2];
                option4Button.GetComponentInChildren<TMP_Text>().text = options[3];

                feedbackText.text = "";

                option1Button.onClick.RemoveAllListeners();
                option2Button.onClick.RemoveAllListeners();
                option3Button.onClick.RemoveAllListeners();
                option4Button.onClick.RemoveAllListeners();

                option1Button.onClick.AddListener(() => CheckAnswer(options[0]));
                option2Button.onClick.AddListener(() => CheckAnswer(options[1]));
                option3Button.onClick.AddListener(() => CheckAnswer(options[2]));
                option4Button.onClick.AddListener(() => CheckAnswer(options[3]));
            } else {
                StartCoroutine(GetQuizQuestion());
            }
            
        }
        else
        {
            questionText.text = "No questions found.";
            feedbackText.text = "";
        }
    }

    public void CheckAnswer(string selectedAnswer)
    {
        
        Debug.Log("Answer received: " + selectedAnswer);
 
        if (selectedAnswer == correctAnswer)
        {
            feedbackText.text = "Correct! Hop on for next Level";
            popupControl.onAnswerSelected("Correct! Hop on for next Level",true);
        }
        else
        {
            feedbackText.text = "Incorrect! The correct answer was: " + correctAnswer;
            popupControl.onAnswerSelected("Incorrect! The correct answer was: " + correctAnswer, false);
        }
    }
}