using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TilemapTrigger : MonoBehaviour
{
    public Image warningImg; // Text hiển thị cảnh báo
    public float warningDuration = 5f;  // Thời gian hiển thị cảnh báo
    private bool isPlayerInside = false;
    private float timeInside = 0f;
    private Coroutine coinPenaltyCoroutine;

    void Start()
    {
        warningImg.gameObject.SetActive(false); // Ẩn text ban đầu
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Nhân vật chạm vào
        {
            isPlayerInside = true;
            timeInside = 0f;
            warningImg.gameObject.SetActive(true); // Hiện thông báo
            StartCoroutine(HideWarningAfterDelay());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timeInside += Time.deltaTime;

            if (timeInside > warningDuration && coinPenaltyCoroutine == null)
            {
                coinPenaltyCoroutine = StartCoroutine(ApplyCoinPenalty());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            warningImg.gameObject.SetActive(false); // Ẩn cảnh báo
            timeInside = 0f;

            if (coinPenaltyCoroutine != null)
            {
                StopCoroutine(coinPenaltyCoroutine);
                coinPenaltyCoroutine = null;
            }
        }
    }

    private IEnumerator HideWarningAfterDelay()
    {
        yield return new WaitForSeconds(warningDuration);
        if (!isPlayerInside) warningImg.gameObject.SetActive(false); // Nếu vẫn còn trong vùng thì không ẩn
    }

    private IEnumerator ApplyCoinPenalty()
    {
        while (isPlayerInside)
        {
            ScoreManagement.instance.RemoveCoin(1); // Trừ 1 coin mỗi giây
            yield return new WaitForSeconds(1f);
        }
    }
}