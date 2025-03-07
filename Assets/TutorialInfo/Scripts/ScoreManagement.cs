using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManagement : MonoBehaviour
{

    public static ScoreManagement instance; // Singleton để truy cập từ script khác
    public TextMeshProUGUI scoreText;  // UI TextMeshPro để hiển thị điểm
    private int score = 0;  // Điểm số

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một GameManager
        }
    }

    public void AddScore(int amount)
    {
        score += amount;  // Cộng điểm
        Debug.Log("Điểm số hiện tại: " + score); // Kiểm tra xem điểm có tăng không
        if (scoreText != null)
        {
            scoreText.text = "" + score;  // Cập nhật UI TextMeshPro
        }
        else
        {
            Debug.LogError("⚠️ UI ScoreText chưa được gán trong GameManager!");
        }
    }
}
