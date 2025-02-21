using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecarmainmenu : MonoBehaviour
{   

    
    public Transform pointA;  // Vị trí bắt đầu
    public Transform pointB;  // Vị trí kết thúc
    public float speed = 2f;  // Tốc độ di chuyển

    private void Start()
    {
        StartCoroutine(MoveLoop());

        if (pointA == null || pointB == null)
        {
            Debug.LogError("⚠️ PointA hoặc PointB chưa được gán trong Inspector!");
            return; // Dừng script nếu chưa gán
        }
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            // Di chuyển từ từ từ A đến B
            while (Vector3.Distance(transform.position, pointB.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
                //yield return null; // Chờ frame tiếp theo
            }

            // Biến mất ngay lập tức
            gameObject.SetActive(false);

            // Dịch chuyển ngay lập tức về A
            transform.position = pointA.position;

            // Xuất hiện lại ngay lập tức
            gameObject.SetActive(true);

            // **Tiếp tục vòng lặp**
        }
    }
}
