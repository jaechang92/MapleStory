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

    private KeyCode key;
    private Dictionary<string, int> keyCodeValuePairs;
    public bool isPress;
    void Start()
    {
        key = GameManager.instance.key;
        keyCodeValuePairs = GameManager.instance.keyCodeValuePairs;
        isPress = GameManager.instance.isPress;
    }

    // Update is called once per frame
    void Update()
    {
        

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            jump = true;
        }

        pressKey = Input.inputString;
        
    }


    

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime , jump);
        jump = false;

        if (isPress)
        {
            Debug.Log(isPress);
        }
        if (key != KeyCode.None)
        {
            controller.PressKey(keyCodeValuePairs[key.ToString()], isPress);
            isPress = false;
        }
    }
}
