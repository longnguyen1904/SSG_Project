/*using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManagement : MonoBehaviour
{
    public int requiredCoinsToWin;
    public static ScoreManagement instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    private int coins = 0;
    public float timeRemaining; 
    private bool isTimerRunning = false;

    public int[] coinCheckpoints; // Mốc coin để mở canvas
    public GameObject[] quizCanvases; // Danh sách canvas câu hỏi
    public GameObject character1, character2, character3; // Nhân vật

    private int lastCheckpointIndex = -1;
    public string Sence;  

    void Awake()
    {
        // Không dùng DontDestroyOnLoad nữa -> reset mỗi scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Reset thời gian khi scene mới bắt đầu
       // timeRemaining = 240f;
        isTimerRunning = false;

        // Cập nhật UI ngay khi scene load
        UpdateCoinUI();
        UpdateTimerUI();
    }

    void Update()
    {
        // Kiểm tra nếu nhân vật xuất hiện thì bắt đầu đếm ngược
        if (!isTimerRunning && CheckCharactersActive())
        {
            StartTimer();
        }

        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                isTimerRunning = false;
                Debug.Log("⏳ Hết thời gian!");

                if (coins >= requiredCoinsToWin)
                {
                    SceneManager.LoadScene(Sence); // Load Level 2 nếu đủ coin
                }
                else
                {
                    SceneManager.LoadScene("gameover"); // Load Game Over nếu thua
                }
            }
        }

        // Mở canvas khi đạt mốc coin
        for (int i = 0; i < coinCheckpoints.Length; i++)
        {
            if (coins >= coinCheckpoints[i] && i > lastCheckpointIndex)
            {
                ActivateCanvas(i);
                lastCheckpointIndex = i;
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

    private bool CheckCharactersActive()
    {
        return (character1 != null && character1.activeInHierarchy) ||
               (character2 != null && character2.activeInHierarchy) ||
               (character3 != null && character3.activeInHierarchy);
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
            Debug.Log($"❌ Mất {amount} coin. Còn lại: {coins}");
        }
        else
        {
            Debug.LogWarning("⚠️ Không đủ coin để trừ!");
        }
    }

    private void ActivateCanvas(int index)
    {
        if (index < quizCanvases.Length && quizCanvases[index] != null)
        {
            quizCanvases[index].SetActive(true);
            Debug.Log($"📌 Mở câu hỏi {index}");
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}  */


using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreManagement : MonoBehaviour
{
    public int requiredCoinsToWin;
    public static ScoreManagement instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    private int coins = 0;
    public float timeRemaining;
    private bool isTimerRunning = false;

    public int[] coinCheckpoints; // Mốc coin để mở câu hỏi
    public GameObject[] allQuizCanvases; // Tất cả câu hỏi
    public GameObject character1, character2, character3; // Nhân vật

    private int lastCheckpointIndex = -1;
    public string sceneName;

    private List<GameObject> availableQuizzes; // Danh sách câu hỏi còn lại

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        isTimerRunning = false;
        availableQuizzes = new List<GameObject>(allQuizCanvases); // Copy danh sách câu hỏi

        UpdateCoinUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isTimerRunning && CheckCharactersActive())
        {
            StartTimer();
        }

        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                isTimerRunning = false;
                Debug.Log("⏳ Hết thời gian!");

                if (coins >= requiredCoinsToWin)
                {
                    SceneManager.LoadScene(sceneName); // Load Level 2 nếu đủ coin
                }
                else
                {
                    SceneManager.LoadScene("gameover"); // Load Game Over nếu thua
                }
            }
        }

        // Kiểm tra mốc coin để mở câu hỏi
        for (int i = 0; i < coinCheckpoints.Length; i++)
        {
            if (coins >= coinCheckpoints[i] && i > lastCheckpointIndex)
            {
                ActivateRandomQuiz();
                lastCheckpointIndex = i;
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

    private bool CheckCharactersActive()
    {
        return (character1 != null && character1.activeInHierarchy) ||
               (character2 != null && character2.activeInHierarchy) ||
               (character3 != null && character3.activeInHierarchy);
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
            Debug.Log($"❌ Mất {amount} coin. Còn lại: {coins}");
        }
        else
        {
            Debug.LogWarning("⚠️ Không đủ coin để trừ!");
        }
    }

    private void ActivateRandomQuiz()
    {
        if (availableQuizzes.Count > 0)
        {
            int randomIndex = Random.Range(0, availableQuizzes.Count); // Lấy một câu hỏi ngẫu nhiên
            GameObject selectedQuiz = availableQuizzes[randomIndex];

            selectedQuiz.SetActive(true);
            Debug.Log($"📌 Hiển thị câu hỏi: {selectedQuiz.name}");

            availableQuizzes.RemoveAt(randomIndex); // Xóa khỏi danh sách để tránh trùng lặp
        }
        else
        {
            Debug.Log("✅ Tất cả câu hỏi đã được sử dụng!");
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}

