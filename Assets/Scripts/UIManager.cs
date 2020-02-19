using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        m_AtiveInvenSortingList = new List<Canvas>();
    }
    

    public GameObject skillUI;
    public List<GameObject> skillSets;
    private KeyCode key;
    public GameObject keyParent;
    public List<KeySetValue> keySets;
    public GameObject m_EmptyDragObject;

    [HideInInspector]
    public GameObject m_NowObject;

    public List<Canvas> m_AtiveInvenSortingList;

    [SerializeField]
    private List<GameObject> m_InvenListAll;
    private void Start()
    {
        foreach (var item in keyParent.GetComponentsInChildren<KeySetValue>())
        {
            keySets.Add(item);
        }
        
    }

    private void Update()
    {
        key = GameManager.instance.key;
        //임시 테스트용
        //if (key == KeyCode.K)
        //{
        //    Debug.Log(key);
        //    //skillUI.SetActive(!skillUI.activeSelf);
        //    m_InvenListAll[0].SetActive(!m_InvenListAll[0].activeSelf);
        //}

        

        //End

    }



    public void SkillTabClickBtn(int i)
    {
        foreach (GameObject item in skillSets)
        {
            if (item.gameObject == skillSets[i])
            {
                item.SetActive(true);
                continue;
            }
            item.SetActive(false);
        }
        
    }
    
    public void InvenSorting(Canvas targetCanvas)
    {
        Canvas temp = targetCanvas;

        for (int j = 0; j < m_AtiveInvenSortingList.Count; j++)
        {
            if(m_AtiveInvenSortingList[j] == temp)
            {
                for (int q = j; q < m_AtiveInvenSortingList.Count -1; q++)
                {
                    m_AtiveInvenSortingList[q] = m_AtiveInvenSortingList[q + 1];
                }
                m_AtiveInvenSortingList[m_AtiveInvenSortingList.Count-1] = temp;

                for (int k = 0; k < m_AtiveInvenSortingList.Count; k++)
                {
                    m_AtiveInvenSortingList[k].sortingOrder = k;
                }


                break;
            }
        }


        int i = 0;

        foreach (var item in m_AtiveInvenSortingList)
        {
            // m_InvenSortingList Canvas를 받아오자
            item.GetComponent<Canvas>().sortingOrder = i;
            i++;
        }
    }
}
