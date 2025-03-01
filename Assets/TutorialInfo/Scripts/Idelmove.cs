using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Idelmove : MonoBehaviour
{
    public Joystick_handle joystick  ;
    public Animator animator;  

    public float moveSpeed;      // Tốc độ di chuyển

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Để lật nhân vật khi xoay

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction = joystick.GetDirection();

        if (direction.magnitude > 0.1f) // Nếu có input từ joystick
        {
            animator.SetBool("run", true);
            rb.velocity = direction * moveSpeed;

            if (direction.x < 0)
                spriteRenderer.flipX = true;  // Nhân vật xoay trái
            else if (direction.x > 0)
                spriteRenderer.flipX = false; // Nhân vật xoay phải
        }
        else
        {
            animator.SetBool("run", false);
            rb.velocity = Vector2.zero; // Dừng lại khi không kéo joystick
        }




    }

    void OnEnable()
    {
        // Khi nhân vật được kích hoạt (SetActive(true)), báo với Timer
        Trafficnumber timer = FindObjectOfType<Trafficnumber>();
        if (timer != null)
        {
            timer.CharacterActivated();
        }
    }
    void OnDisable()
    {
        // Khi nhân vật bị vô hiệu hóa (SetActive(false)), báo với Timer
        Trafficnumber timer = FindObjectOfType<Trafficnumber>();
        if (timer != null)
        {
            timer.CharacterDeactivated();
        }
    }

}
