using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;  


public class Trafficnumber : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private int colorIndex = 0;
    private float elapsedTime = 0f;
    private float colorTimeLeft;
    private bool isStarted = false;
    private int activeCharacterCount = 0; // Đếm số lượng nhân vật đang active

    private Color[] colors = { Color.red, Color.green, Color.yellow };
    private float[] colorDurations = { 5f, 5f, 3f };

    void Start()
    {
        if (timeText == null)
        {
            timeText = GetComponent<TextMeshProUGUI>();
        }
        timeText.text = "0"; // Chưa có nhân vật active => hiện 0
    }

    void Update()
    {
        if (!isStarted) return;

        elapsedTime += Time.deltaTime;
        timeText.text = Mathf.FloorToInt(elapsedTime).ToString();

        if (elapsedTime >= colorTimeLeft)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        colorIndex = (colorIndex + 1) % colors.Length;
        SetColor(colorIndex);
    }

    void SetColor(int index)
    {
        timeText.color = colors[index];
        elapsedTime = 0f;
        colorTimeLeft = colorDurations[index];
    }

    // Khi có nhân vật được SetActive(true)
    public void CharacterActivated()
    {
        activeCharacterCount++;
        if (!isStarted)
        {
            isStarted = true;
            SetColor(colorIndex);
        }
    }

    // Khi một nhân vật bị SetActive(false)
    public void CharacterDeactivated()
    {
        activeCharacterCount--;
        if (activeCharacterCount <= 0)
        {
            isStarted = false;
            elapsedTime = 0f;
            timeText.text = "0";
        }
    }
}
