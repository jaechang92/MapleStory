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
    
    // 퍼블릭 변수
    public GameObject skillUI;
    public List<GameObject> skillSets;
    public GameObject keyParent;
    public List<KeySetValue> keySets;
    public GameObject m_EmptyDragObject;
    public GameObject visibleObject;
    public Image expImage;


    [HideInInspector]
    public GameObject m_NowObject;

    public List<Canvas> m_AtiveInvenSortingList;

    [SerializeField]
    private List<GameObject> m_InvenListAll;

    private KeyCode key;
    private Text visibleObjectInText;

    private List<string> m_texts;
    private NPCController nowNPC;
    private void Start()
    {
        foreach (var item in keyParent.GetComponentsInChildren<KeySetValue>())
        {
            keySets.Add(item);
        }

        visibleObjectInText = visibleObject.GetComponentInChildren<Text>();

        foreach (var item in m_InvenListAll)
        {
            item.SetActive(false);
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


    private int textCount = 0;
    public void GetText(List<string> texts,NPCController nPCController)
    {
        m_texts = texts;
        visibleObject.SetActive(true);
        NextText();
        nowNPC = nPCController;
    }


    public void NextText()
    {
        if (textCount < m_texts.Count)
        {
            visibleObjectInText.text = m_texts[textCount];
        }
        else
        {
            visibleObject.SetActive(false);
            textCount = 0;
            nowNPC.questNum++;
            return;
        }
        textCount++;
        Debug.Log(textCount);
    }

    public void UpdateExp(int nowExp, int maxExp)
    {
        expImage.fillAmount = nowExp / maxExp;
    }


}
