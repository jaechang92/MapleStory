using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            keyCodeValuePairs.Add(keyCodes[i].ToString(), i);
        }
        Debug.Log(keyCodeValuePairs.Count);
    }
    public bool isPress { get; set; } = false;
    public bool _isPress;
    public Dictionary<string, int> keyCodeValuePairs = new Dictionary<string, int>();
    private KeyCode[] keyCodes = {  KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,
                                    KeyCode.H,KeyCode.I,KeyCode.J,KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,
                                    KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,KeyCode.U,
                                    KeyCode.V,KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z,
                                    KeyCode.LeftControl,KeyCode.LeftAlt,KeyCode.RightControl,KeyCode.RightAlt,
                                    KeyCode.Space,
                                    KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,
                                    KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9,
                                    KeyCode.Minus,KeyCode.Equals,KeyCode.BackQuote , KeyCode.LeftShift, KeyCode.RightShift, KeyCode.Backslash};

    public KeyCode key;


    private void Update()
    {
        _isPress = isPress;
        if (Input.anyKeyDown)
        {
            foreach (KeyCode item in keyCodes)
            {
                if (Input.GetKeyDown(item))
                {
                    key = item;
                    isPress = true;
                }
            }
            
        }
        //key = KeyCode.None;
    }
    //눌리면 bool 참거짓 작성

}
