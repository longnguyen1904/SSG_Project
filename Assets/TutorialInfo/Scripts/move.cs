using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  

public class move : MonoBehaviour
{

    float movespeed = 4f; 
    Animator animator;
    float Directionx = Input.GetAxisRaw("Horizontal");
    float Directiony = Input.GetAxisRaw("Vertical");
    // Start is called before the first frame update
    void Start()
    {
        
       animator = GetComponent<Animator>();

          
        
    }

    // Update is called once per frame
    void Update()
    {    if (Directionx != null || Directiony != null)
        {
            animator.SetFloat("speed", 4);

            float movestepX = Directionx * movespeed * Time.deltaTime;
            float movestepY = Directiony * movespeed * Time.deltaTime;
            transform.Translate(movestepX, movestepY, 0);
        }
        else  {
            animator.SetFloat("speed", 0); 
            
        
        }
        
    }
}
