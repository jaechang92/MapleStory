using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController2D controller;

    public float speed = 0.0f;
    private float horizontalMove = 0.0f;
    private bool jump = false;
    
    private string pressKey;
    private bool isPress = false;

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            jump = true;
        }
        
        if (Input.anyKeyDown)
        {
            pressKey = Input.inputString;
            isPress = true;
        }

        

    }


    

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime , jump);
        jump = false;

        controller.Attack(pressKey, isPress);
        isPress = false;
    }
}
