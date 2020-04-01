using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    static public EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
