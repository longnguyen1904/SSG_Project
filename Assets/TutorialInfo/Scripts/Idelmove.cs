using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iđelmove : MonoBehaviour
{
    public Joystick_handle joystick;

   
    public float moveSpeed = 2f;    // Tốc độ di chuyển

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Để lật nhân vật khi xoay

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Lấy Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer để flip
    }

    void Update()
    {
        Vector2 direction = joystick.GetDirection(); // Lấy hướng từ joystick
        rb.velocity = direction * moveSpeed; // Di chuyển nhân vật

        if (direction.x < 0) // Nếu di chuyển sang trái
        {
            spriteRenderer.flipX = true; // Lật nhân vật sang trái
        }
        else if (direction.x > 0) // Nếu di chuyển sang phải
        {
            spriteRenderer.flipX = false; // Giữ nhân vật hướng phải
        }
    }
}
