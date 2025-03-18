using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Traffic_number_setting : MonoBehaviour
{
    public TMP_Text redText, yellowText, greenText;
    public Image redLightWalk, greenLightWalk;  
    public GameObject[] characters;
    public float redDuration = 5f, yellowDuration = 2f, greenDuration = 3f;

    public bool isGreenLight = false; // Kiểm tra đèn xanh
    public bool isRedLight = false; // Kiểm tra đèn đỏ

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
            yield return StartCoroutine(RunTrafficLight(redText, redDuration, false, true));  // Đèn đỏ
            yield return StartCoroutine(RunTrafficLight(yellowText, yellowDuration, true, false)); // Đèn vàng (giống đèn xanh)
            yield return StartCoroutine(RunTrafficLight(greenText, greenDuration, true, false));  // Đèn xanh
        }
    }

    IEnumerator RunTrafficLight(TMP_Text lightText, float duration, bool greenState, bool redState)
    {
        redText.gameObject.SetActive(lightText == redText);
        yellowText.gameObject.SetActive(lightText == yellowText);
        greenText.gameObject.SetActive(lightText == greenText);
        greenLightWalk.gameObject.SetActive(lightText == redText);
        redLightWalk.gameObject.SetActive(lightText == greenText);  

        isGreenLight = greenState; // Cập nhật trạng thái đèn xanh
        isRedLight = redState;     // Cập nhật trạng thái đèn đỏ

        float timer = duration;
        while (timer > 0)
        {
            lightText.text = Mathf.CeilToInt(timer).ToString();
            yield return new WaitForSeconds(1f);
            timer--;
        }
    }
}