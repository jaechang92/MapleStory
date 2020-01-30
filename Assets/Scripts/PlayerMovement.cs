using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float speed = 0.0f;
    private float horizontalMove = 0.0f;
    private float VerticalMove = 0.0f;
    private bool jump = false;

    private string pressKey;

    private KeyCode key;
    private Dictionary<string, int> keyCodeValuePairs;
    public bool isPress;

    void Start()
    {
        
        keyCodeValuePairs = GameManager.instance.keyCodeValuePairs;
        
    }

    // Update is called once per frame
    void Update()
    {
        isPress = GameManager.instance.isPress;
        key = GameManager.instance.key;

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        VerticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            jump = true;
        }

        pressKey = Input.inputString;
        
    }


    

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, VerticalMove * Time.fixedDeltaTime, jump);
        jump = false;

        if (isPress)
        {
            //Debug.Log(isPress);
        }
        if (key != KeyCode.None)
        {
            //Debug.Log(key);
            controller.PressKey(keyCodeValuePairs[key.ToString()], isPress);
            GameManager.instance.isPress = false;
            //Debug.Log(GameManager.instance.isPress);
            GameManager.instance.key = KeyCode.None;
        }
    }
}
