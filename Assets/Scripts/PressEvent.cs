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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        ImageSnap();
        if (!this.gameObject.CompareTag("TitleBar"))
        {
            m_transform.position = m_v3;
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

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        if (isPress)
        {
            m_transform.position = Input.mousePosition - mouseToObjectDistance;
        }
    }

    void ImageSnap()
    {
        for (int i = 0; i < 44; i++)
        {
            if (UIManager.instance.keySets[i].transform.position.x - UIManager.instance.keySets[i].GetComponent<RectTransform>().sizeDelta.x * 0.5f <= m_transform.position.x &&
                UIManager.instance.keySets[i].transform.position.x + UIManager.instance.keySets[i].GetComponent<RectTransform>().sizeDelta.x * 0.5f >= m_transform.position.x &&
                UIManager.instance.keySets[i].transform.position.y - UIManager.instance.keySets[i].GetComponent<RectTransform>().sizeDelta.y * 0.5f <= m_transform.position.y &&
                UIManager.instance.keySets[i].transform.position.y + UIManager.instance.keySets[i].GetComponent<RectTransform>().sizeDelta.y * 0.5f >= m_transform.position.y)
            {
                Debug.Log("Snap");
                UIManager.instance.keySets[i].GetImage(this.gameObject.GetComponent<Skill>().skillNum);
            }
            
        }

    }
}
