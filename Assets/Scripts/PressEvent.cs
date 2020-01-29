using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        mouseToObjectDistance = Input.mousePosition - m_transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }



    public Transform m_transform;
    public bool isPress = false;
    private Vector3 mouseToObjectDistance = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
        m_transform = this.gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPress)
        {
            m_transform.position = Input.mousePosition - mouseToObjectDistance;
        }
    }
}
