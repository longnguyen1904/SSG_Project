using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvas : MonoBehaviour
{

    public Button[] answerButtons; // 4 đáp án
    public Button correctButton; // Button đúng (kéo vào trong Unity)

    private ScoreManagement scoreManager;

    void Start()
    {
        scoreManager = ScoreManagement.instance;

        foreach (Button button in answerButtons)
        {
            button.onClick.AddListener(() => OnAnswerSelected(button));
        }
    }

    void OnAnswerSelected(Button clickedButton)
    {
        if (clickedButton == correctButton) // Nếu chọn đúng
        {
            Debug.Log("✅ Đáp án đúng! +2 Coin");
            scoreManager.AddCoin(2);
            gameObject.SetActive(false); // Ẩn canvas khi đúng
        }
        else // Nếu chọn sai
        {
            Debug.Log("❌ Sai! -1 Coin");
            scoreManager.RemoveCoin(1);
            StartCoroutine(FlashRedEffect(clickedButton)); // Nháy đỏ
        }
    }

    private IEnumerator FlashRedEffect(Button button)
    {
        Color originalColor = button.image.color;
        button.image.color = new Color(1, 0, 0, 0.5f); // Đỏ nhẹ
        yield return new WaitForSeconds(0.1f);
        button.image.color = originalColor; // Trả về màu cũ
    }
}

/*public Button[] answerButtons; // Danh sách 4 button đáp án
    public int correctAnswerIndex; // Chỉ số của đáp án đúng (0 - 3)

    private ScoreManagement scoreManager;

    void Start()
    {
        scoreManager = ScoreManagement.instance;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // Cần biến tạm để tránh lỗi delegate
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    void OnAnswerSelected(int index)
    {
        if (index == correctAnswerIndex) // Nếu chọn đúng
        {
            Debug.Log("✅ Đáp án đúng! +2 Coin");
            scoreManager.AddCoin(2);
            gameObject.SetActive(false); // Tắt canvas khi đúng
        }
        else
        {
            Debug.Log("❌ Sai! -1 Coin");
            scoreManager.RemoveCoin(1);
            StartCoroutine(FlashRedEffect(answerButtons[index])); // Hiệu ứng nhấp nháy đỏ
        }
    }

    private IEnumerator FlashRedEffect(Button button)
    {
        Color originalColor = button.image.color;
        button.image.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        button.image.color = originalColor;
    }  */