using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    // Tham chiếu đến 3 nhân vật trong Hierarchy
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;

    private int score = 0;
    private float timeRemaining = 120f; // 2 phút
    private bool isTimerRunning = false;
    private bool hasStartedCountdown = false; // Kiểm tra xem đã bắt đầu đếm ngược chưa

    private QuestionMask currentQuestion;
    private string correctAnswer = "A";

    void Start()
    {
        buttonA.onClick.AddListener(() => CheckAnswer("A"));
        buttonB.onClick.AddListener(() => CheckAnswer("B"));
        buttonC.onClick.AddListener(() => CheckAnswer("C"));
        buttonD.onClick.AddListener(() => CheckAnswer("D"));

        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        // Kiểm tra nếu ít nhất 1 nhân vật đã spawn và chưa bắt đầu đếm ngược
        if (!hasStartedCountdown && CheckCharactersSpawned())
        {
            StartCountdown();
        }

        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();

            if (timeRemaining <= 0)
            {
                isTimerRunning = false;
                CheckGameOver();
            }
        }
    }

    public void StartQuiz(QuestionMask question)
    {
        currentQuestion = question;
        SetButtonsInteractable(true);
        // Đếm ngược sẽ bắt đầu khi nhân vật spawn, không phải ở đây
    }

    void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            score += 10;
            UpdateScoreText();

            currentQuestion.quizCanvas.gameObject.SetActive(false);
            currentQuestion.HideQuestion();
            isTimerRunning = false; // Dừng đếm ngược khi trả lời đúng

            Debug.Log("Đáp án đúng!");
        }
        else
        {
            Debug.Log("Đáp án sai!");
        }

        SetButtonsInteractable(false);
    }

    bool CheckCharactersSpawned()
    {
        // Kiểm tra xem ít nhất 1 trong 3 nhân vật có active trong Hierarchy không
        return (character1 != null && character1.activeInHierarchy) ||
               (character2 != null && character2.activeInHierarchy) ||
               (character3 != null && character3.activeInHierarchy);
    }

    void StartCountdown()
    {
        hasStartedCountdown = true;
        timeRemaining = 120f; // Reset thời gian về 2 phút
        isTimerRunning = true;
        Debug.Log("Bắt đầu đếm ngược!");
    }

    void UpdateScoreText()
    {
        scoreText.text =  score.ToString();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SetButtonsInteractable(bool state)
    {
        buttonA.interactable = state;
        buttonB.interactable = state;
        buttonC.interactable = state;
        buttonD.interactable = state;
    }

    void CheckGameOver()
    {
        if (score < 10)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
