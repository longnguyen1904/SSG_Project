using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Traffic_number_setting : MonoBehaviour
{
    public TMP_Text redText, yellowText, greenText;
    public GameObject[] characters;
    public float redDuration = 5f, yellowDuration = 2f, greenDuration = 3f;

    public bool isGreenLight = false; // Kiểm tra đèn xanh

    private bool isTrafficRunning = false;

    void Update()
    {
        if (!isTrafficRunning && AnyCharacterActive())
        {
            StartCoroutine(TrafficLightCycle());
            isTrafficRunning = true;
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
        while (true)
        {
            yield return StartCoroutine(RunTrafficLight(redText, redDuration, false));
            yield return StartCoroutine(RunTrafficLight(yellowText, yellowDuration, false));
            yield return StartCoroutine(RunTrafficLight(greenText, greenDuration, true));
        }
    }

    IEnumerator RunTrafficLight(TMP_Text lightText, float duration, bool greenState)
    {
        redText.gameObject.SetActive(lightText == redText);
        yellowText.gameObject.SetActive(lightText == yellowText);
        greenText.gameObject.SetActive(lightText == greenText);

        isGreenLight = greenState; // Cập nhật trạng thái đèn xanh

        float timer = duration;
        while (timer > 0)
        {
            lightText.text = Mathf.CeilToInt(timer).ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }
    }

    public bool IsGreenLight() // 📌 Thêm phương thức này để kiểm tra trạng thái đèn
    {
        return isGreenLight;
    }
}
