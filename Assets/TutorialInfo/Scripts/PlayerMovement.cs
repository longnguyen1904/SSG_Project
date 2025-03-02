using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FloatingJoystick joystick; // Joystick để lấy input
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public Rigidbody rb; // Rigidbody nhân vật

    private void FixedUpdate()
    {
        // Lấy hướng từ Joystick (trục X và Z)
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        // Di chuyển nhân vật theo hướng Joystick
        rb.velocity = moveDirection * moveSpeed;

        // Nếu có hướng di chuyển, xoay nhân vật theo hướng đó
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
