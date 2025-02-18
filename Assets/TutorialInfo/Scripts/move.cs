using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  

public class move : MonoBehaviour
{

    float movespeed = 2f; 
    Animator animator;

    public Vector3 moveinput;  
    
    // Start is called before the first frame update
    void Start()
    {
        
       animator = GetComponent<Animator>();

          
        
    }

    // Update is called once per frame
    void Update()
    {
        moveinput.x = Input.GetAxis("Horizontal");
        moveinput.y = Input.GetAxis("Vertical");
        transform.position += moveinput * movespeed * Time.deltaTime;

        if (moveinput.x != 0 || moveinput.y != 0) {
            
            animator.SetBool("run", true);
        }
        else {
            animator.SetBool("run", false); }



        if (moveinput.x > 0) // Đi sang phải
        {
            transform.localScale = new Vector3(4, 4, 4);
        }
        else if (moveinput.x < 0) // Đi sang trái
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }
    }
}
