using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{


    /*
    public static QuizManager instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    private int coins = 0;
    private float timeRemaining = 120f;
    private bool isTimerRunning = false;

    public int[] coinCheckpoints; // Mốc coin để mở canvas
    public GameObject[] quizCanvases; // Danh sách các canvas câu hỏi

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                isTimerRunning = false;
                Debug.Log("⏳ Hết thời gian!");
            }
        }

        // Kiểm tra coin để mở canvas
        for (int i = 0; i < coinCheckpoints.Length; i++)
        {
            if (coins == coinCheckpoints[i] && !quizCanvases[i].activeSelf)
            {
                quizCanvases[i].SetActive(true);
                Debug.Log("📌 Mở câu hỏi!");
            }
        }
    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            Debug.Log("⏳ Bắt đầu đếm ngược!");
        }
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateCoinUI();
        Debug.Log("💰 Coin hiện tại: " + coins);
    }

    public void RemoveCoin(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinUI();
            Debug.Log("❌ Mất " + amount + " coin. Còn lại: " + coins);
        }
        else
        {
            Debug.LogWarning("⚠️ Không đủ coin để trừ!");
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coins;
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }   */
}
