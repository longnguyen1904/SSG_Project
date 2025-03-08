using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class Coin : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("🚀 Va chạm xảy ra với: " + other.gameObject.name); // Debug để kiểm tra va chạm

        if (other.CompareTag("Coin"))  // Kiểm tra nếu va chạm với Coin
        {
            Debug.Log("✅ Nhặt Coin! Cộng điểm...");

            if (ScoreManagement.instance != null)
            {
                ScoreManagement.instance.AddScore(1); // Cộng điểm
                Debug.Log("🎯 Điểm cộng thành công!");
            }
            else
            {
                Debug.LogError("⚠️ ScoreManagement chưa được gán!");
            }

            Destroy(other.gameObject); // Xóa Coin khỏi Scene
            Debug.Log("💨 Coin đã biến mất!");
        }
    }
}
 /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Kiểm tra nếu chạm vào Coin
        { 
            Debug.Log("long sờ chim");  
            ScoreManagement.instance.AddScore(1); // Gọi hàm cộng điểm

            Debug.Log("long sờ bướm"); 
            Destroy(other.gameObject); // Xóa Coin khỏi Scene
        }
    } */