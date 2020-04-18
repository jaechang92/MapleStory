using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GameObject acceptAndReject;

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
    public bool m_QusetBool = false;

    private void TextInit(List<string> texts)
    {
        m_texts = texts;
        visibleObject.SetActive(true);
        textCount = 0;
    }

    public void GetText(List<string> texts,NPCController nPCController, bool isQuest)
    {

        TextInit(texts);
        m_QusetBool = isQuest;
        nowNPC = nPCController;
        NextText();
    }
    
    public void GetText(List<string> texts, NPCController nPCController)
    {

        TextInit(texts);
        m_QusetBool = false;
        nowNPC = nPCController;
        NextText();
    }

    
    //public void NextText()
    //{
    //    if (textCount < m_texts.Count - 1)
    //    {
    //        visibleObjectInText.text = m_texts[textCount];
    //    }
    //    else if (textCount < m_texts.Count)
    //    {
    //        Debug.Log(textCount);
    //        Debug.Log(m_texts.Count);
    //        if (m_QusetBool)
    //        {
    //            acceptAndReject.SetActive(true);
    //        }
    //        visibleObjectInText.text = m_texts[m_texts.Count - 1];

    //    }
    //    else
    //    {
    //        textCount = 0;
    //        nowNPC.questNum++;
    //        visibleObject.SetActive(false);
    //        return;
    //    }
    //    textCount++;
    //    Debug.Log(textCount);
    //}
    
        
    public void NextText()
    {
        Debug.Log("textCount = " + textCount);
        Debug.Log("m_texts.Count = " + m_texts.Count);

        if (textCount == m_texts.Count - 1 && m_QusetBool)
        {
            acceptAndReject.SetActive(true);
        }
        if (textCount > m_texts.Count -1)
        {
            nowNPC.questNum++;
            visibleObject.SetActive(false);
            return;
        }

        visibleObjectInText.text = m_texts[textCount];
        textCount++;
    }


    public void UpdateExp(int nowExp, int maxExp)
    {
        expImage.fillAmount = nowExp / maxExp;
    }


    public void Accept()
    {
        QuestAdd();
        nowNPC.questNum++;
        nowNPC.isQuest = true;
        acceptAndReject.SetActive(false);
        visibleObject.SetActive(false);
    }
    public void Reject()
    {
        acceptAndReject.SetActive(false);
        visibleObject.SetActive(false);
    }

    public void QuestAdd()
    {
        nowNPC.questIndex = EventManager.instance.Added(nowNPC.thisNPCNumber,nowNPC.NPCTalk[nowNPC.questNum].questMonsterID, nowNPC.NPCTalk[nowNPC.questNum].killCount, nowNPC.NPCTalk[nowNPC.questNum].reward);
        
        //nowNPC.NPCTalk
    }


}
