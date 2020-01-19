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

    public Dictionary<string, int> keyCodeValuePairs = new Dictionary<string, int>();
    private KeyCode[] keyCodes = {  KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,
                                    KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,
                                    KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,
                                    KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z,
                                    KeyCode.LeftControl,KeyCode.LeftAlt,KeyCode.RightControl,KeyCode.RightAlt,
                                    KeyCode.Space,
                                    KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,
                                    KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,
                                    KeyCode.Minus,KeyCode.Equals,KeyCode.BackQuote};

    public KeyCode key;

    private void Awake()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            keyCodeValuePairs.Add(keyCodes[i].ToString(), i);
        }
    }

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
            foreach (KeyCode item in keyCodes)
            {
                if (Input.GetKeyDown(item))
                {
                    key = item;
                }
            }
            pressKey = Input.inputString;
            isPress = true;
        }



    }


    

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime , jump);
        jump = false;

        if (key != KeyCode.None)
        {
            controller.Attack(keyCodeValuePairs[key.ToString()], isPress);
            isPress = false;
        }
    }
}
