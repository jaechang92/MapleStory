using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        m_v3 = m_transform.position;
        mouseToObjectDistance = Input.mousePosition - m_transform.position;

        if (!this.gameObject.CompareTag("TitleBar"))
        { 
            UIManager.instance.m_EmptyDragObject.SetActive(true);
            UIManager.instance.m_EmptyDragObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        if(!this.gameObject.CompareTag("TitleBar"))
        {
            if (UIManager.instance.m_NowObject != null)
            {
                ImageSnap();
            }
            UIManager.instance.m_EmptyDragObject.SetActive(false);
            UIManager.instance.m_NowObject = null;
        }

        
    }



    public Transform m_transform;
    public Vector3 m_v3;
    public bool isPress = false;
    private Vector3 mouseToObjectDistance = new Vector3();

    private void Awake()
    {
        m_transform = this.gameObject.transform;

    }
    

    // Update is called once per frame
    private void Update()
    {
        if (isPress)
        {
            if (this.gameObject.CompareTag("TitleBar"))
            {
                this.gameObject.transform.position = Input.mousePosition - mouseToObjectDistance;
            }
            else
            {
                UIManager.instance.m_EmptyDragObject.transform.position = Input.mousePosition - mouseToObjectDistance;
            }
            UIManager.instance.InvenSorting(this.gameObject.GetComponentInParent<AtiveUI>().m_Canvas);
        }
    }

    void ImageSnap()
    {
        UIManager.instance.m_NowObject.GetComponent<KeySetValue>().GetInit(this.gameObject);
        //UIManager.instance.m_NowObject.GetComponent<KeySetValue>().GetImage(this.gameObject.GetComponent<Skill>().skillNum);
        //UIManager.instance.keySets[i].GetImage(this.gameObject.GetComponent<Skill>().skillNum);
            
    }

    
}
