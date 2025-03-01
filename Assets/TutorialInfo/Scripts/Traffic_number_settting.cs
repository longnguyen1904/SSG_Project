using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Traffic_number_settting : MonoBehaviour
{
    public TMP_Text redText, yellowText, greenText; // Dùng TMP_Text thay vì Text
    public GameObject[] characters; // Danh sách nhân vật
    public float redDuration = 5f, yellowDuration = 2f, greenDuration = 3f; // Thời gian từng đèn

    private bool isTrafficRunning = false; // Kiểm tra trạng thái hoạt động

    void Update()
    {
        if (!isTrafficRunning && AnyCharacterActive())
        {
            StartCoroutine(TrafficLightCycle());
            isTrafficRunning = true; // Đảm bảo chỉ chạy 1 lần
        }
    }

    bool AnyCharacterActive()
    {
        foreach (GameObject character in characters)
        {
            if (character.activeSelf) return true;
        }
        return false;
    }

    IEnumerator TrafficLightCycle()
    {
        while (true) // Lặp vô hạn khi nhân vật còn hoạt động
        {
            yield return StartCoroutine(RunTrafficLight(redText, redDuration));
            yield return StartCoroutine(RunTrafficLight(yellowText, yellowDuration));
            yield return StartCoroutine(RunTrafficLight(greenText, greenDuration));
        }
    }

    IEnumerator RunTrafficLight(TMP_Text lightText, float duration) // Sửa từ Text thành TMP_Text
    {
        redText.gameObject.SetActive(lightText == redText);
        yellowText.gameObject.SetActive(lightText == yellowText);
        greenText.gameObject.SetActive(lightText == greenText);

        float timer = duration;
        while (timer > 0)
        {
            lightText.text = Mathf.CeilToInt(timer).ToString(); // Hiển thị số giây còn lại
            yield return new WaitForSeconds(1f);
            timer--;
        }
    }
}
