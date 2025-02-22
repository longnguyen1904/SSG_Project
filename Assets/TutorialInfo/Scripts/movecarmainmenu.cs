using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecarmainmenu : MonoBehaviour
{


    public Vector3 pointA = new Vector3(-3, 0, 0); // Điểm A (có thể thay đổi)
    public Vector3 pointB = new Vector3(3, 0, 0);  // Điểm B (có thể thay đổi)
    public float speed = 2f;  // Tốc độ di chuyển

    private void Start()
    {
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(pointA, pointB)); // Di chuyển từ A đến B
            gameObject.GetComponent<Renderer>().enabled = false; // Ẩn nhân vật
            yield return new WaitForSeconds(0.5f); // Đợi 0.5s
            transform.position = pointA; // Đưa về A
            gameObject.GetComponent<Renderer>().enabled = true; // Hiện nhân vật lại
        }
    }

    IEnumerator MoveToPosition(Vector3 start, Vector3 end)
    {
        float journey = 0f;
        while (journey < 1f)
        {
            journey += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, end, journey);
            yield return null;
        }
    }
}
