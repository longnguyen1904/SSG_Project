using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMask : MonoBehaviour
{
    public Canvas quizCanvas;
    public QuizManager quizManager;

    private void Start()
    {
        quizCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            quizCanvas.gameObject.SetActive(true);
            quizManager.StartQuiz(this);
        }
    }

    public void HideQuestion()
    {
        gameObject.SetActive(false);
    }
}
