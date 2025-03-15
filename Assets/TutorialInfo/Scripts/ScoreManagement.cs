using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManagement : MonoBehaviour
{
    public int requiredCoinsToWin;

    public static ScoreManagement instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    public int coins = 0;
    public float timeRemaining = 120f;
    public bool isTimerRunning = false;
    public int[] coinCheckpoints; // Mốc coin để mở canvas
    public GameObject[] quizCanvases; // Danh sách canvas câu hỏi
    public GameObject character1, character2, character3; // Nhân vật

    private int lastCheckpointIndex = -1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
                if (coins >= requiredCoinsToWin) // requiredCoinsToWin là mốc coin public
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("Winlv1"); // Load Level 2 nếu đủ coin

                }
                else
                {
                    SceneManager.LoadScene("gameover"); // Load Game Over nếu thua
                }
            }


        }

        /*if (timeRemaining <= 0)
        {
            isTimerRunning = false;
            Debug.Log("⏳ Hết thời gian!");

            if (coins >= requiredCoinsToWin) // requiredCoinsToWin là mốc coin public
            {
                SceneManager.LoadScene("lv2"); // Load Level 2 nếu đủ coin
            }
            else
            {
                SceneManager.LoadScene("gameover"); // Load Game Over nếu thua
            }
        }*/

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
            Debug.Log("❌ Mất " + amount + " coin. Còn lại: " + coins);
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
            Debug.Log("📌 Mở câu hỏi " + index);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "" + coins;
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
/*public static ScoreManagement instance;

public TextMeshProUGUI coinText;
public TextMeshProUGUI timerText;

private int coins = 0;
private float timeRemaining = 120f;
private bool isTimerRunning = false;
private int lastCheckpoint = -1;

public int[] coinCheckpoints; // Các mốc Coin mở Canvas
public GameObject[] quizCanvases; // Danh sách Canvas câu hỏi
public GameObject character1, character2, character3; // Nhân vật

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

void Start()
{
    UpdateCoinUI();
    UpdateTimerUI();
    Debug.Log("📌 Mốc coin cần đạt: " + string.Join(", ", coinCheckpoints));
}

void Update()
{
    // Kiểm tra nếu có nhân vật thì bắt đầu đếm ngược
    if (!isTimerRunning && CheckCharactersActive())
    {
        StartTimer();
    }

    // Nếu đang chạy, giảm thời gian
    if (isTimerRunning)
    {
        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        // Nếu hết giờ, dừng game
        if (timeRemaining <= 0)
        {
            isTimerRunning = false;
            Debug.Log("⏳ Hết thời gian!");
            GameOver();
        }
    }

    // Kiểm tra nếu đạt mốc Coin thì mở Canvas
    for (int i = 0; i < coinCheckpoints.Length; i++)
    {
        if (coins >= coinCheckpoints[i] && lastCheckpoint < i)
        {
            lastCheckpoint = i;
            ActivateCanvas(i);
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
        Debug.Log("❌ Mất " + amount + " coin. Còn lại: " + coins);
    }
    else
    {
        Debug.LogWarning("⚠️ Không đủ coin để trừ!");
    }
}

private void ActivateCanvas(int index)
{
    if (index < quizCanvases.Length)
    {
        quizCanvases[index].SetActive(true);
        Debug.Log("📌 Mở câu hỏi: " + quizCanvases[index].name);
    }
}

public void AnswerQuestion(GameObject canvas, bool isCorrect)
{
    if (isCorrect)
    {
        AddCoin(2); // Trả lời đúng thì cộng coin
        canvas.SetActive(false);
        Debug.Log("✅ Đáp án đúng! +2 Coin");
    }
    else
    {
        RemoveCoin(1); // Trả lời sai thì trừ coin
        Debug.Log("❌ Đáp án sai! -1 Coin");
    }
}

private void GameOver()
{
    Debug.Log("💀 Trò chơi kết thúc! Hết thời gian!");
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



