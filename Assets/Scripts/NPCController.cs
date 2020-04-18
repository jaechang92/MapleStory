using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public int thisNPCNumber;
    public enum NPCType
    {
        Normal,CanQuest ,CanTalk
    }

    public NPCType m_NPC;
    
    
    [System.Serializable]
    public struct TextList
    {
        public List<string> stringList;
        public bool isQuest;
        public int questMonsterID;
        public int killCount;
        public EventManager.reward reward;
    }


    public List<TextList> NPCTalk;

    [System.Serializable]
    public struct ClearUnClear
    {
        public List<string> stringList;
    }

    public List<ClearUnClear> questClearUnClear;

    public int questNum = 0;
    public int questIndex = -1;
    public bool isQuest;

    public List<string> storyStringList;


    void Start()
    {
        this.gameObject.layer = 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void Talk()
    {
        switch (m_NPC)
        {
            case NPCType.Normal:
                Debug.Log("아무일도 일어나지 않습니다.");
                break;
            case NPCType.CanQuest:
                Debug.Log("퀘스트 작성");
                ///
                /// 퀘스트에는 사냥후 전리품 제출퀘스트
                /// 사냥해야될 몬스터를 알려주고 제출하는 물품 숫자 체크
                /// 퀘스트 분기
                /// 퀘스트를 받기전
                /// 퀘스트를 받은후
                ///

                ///
                /// 퀘스트 옵저버 구현
                ///


                // 옵저버에 전달
                EventManager.instance.talkEvent += SendToObserver;
                EventManager.instance.OnTalkEvent();
                EventManager.instance.talkEvent -= SendToObserver;
                //if (questNum < NPCTalk.Count)
                //{
                //    UIManager.instance.GetText(NPCTalk[questNum].stringList, this.gameObject.GetComponent<NPCController>(), NPCTalk[questNum].isQuest);
                //}
                //else
                //{
                //    for (int i = 0; i < EventManager.instance.quests.Count; i++)
                //    {
                //        Debug.Log(this.gameObject.name == EventManager.instance.quests[i].nowNPC.name);
                //        if (this.gameObject.name == EventManager.instance.quests[i].nowNPC.name)
                //        {
                //            if (EventManager.instance.quests[i].isClear)
                //            {
                //                EventManager.instance.RemoveQuestList(EventManager.instance.quests[questIndex]);
                //                questIndex = -1;
                //                GameManager.instance.m_CharacterController2D.GetReward(NPCTalk[questNum-1].reward);
                //                questNum++;
                //            }
                //            else
                //            {
                //                UIManager.instance.GetText(questUnClear, this.gameObject.GetComponent<NPCController>());
                //            }
                //        }
                //    }
                //}


                //else if(NPCTalk[questNum].isClear)
                //{
                //    EventManager.instance.RemoveQuestList(EventManager.instance.quests[questIndex]);
                //    questIndex = -1;
                //    GameManager.instance.m_CharacterController2D.GetReward(NPCTalk[questNum].reward);
                //    questNum++;
                //}


                //questNum++;

                break;
            case NPCType.CanTalk:
                Debug.Log("스토리 이야기");
                UIManager.instance.GetText(storyStringList, this.gameObject.GetComponent<NPCController>());
                questNum = NPCTalk.Count;

                break;
            default:
                break;
        }
    }

    

    private void SendToObserver()
    {

        if (isQuest)
        {
            for (int i = 0; i < EventManager.instance.quests.Count; i++)
            {
                if (EventManager.instance.quests[i].nowNPC == this.gameObject.GetComponent<NPCController>().thisNPCNumber)
                {
                    if (EventManager.instance.quests[i].isClear)
                    {
                        // 클리어 텍스트
                        UIManager.instance.GetText(questClearUnClear[1].stringList, this.gameObject.GetComponent<NPCController>());
                        EventManager.instance.RemoveQuestList(EventManager.instance.quests[i]);
                        isQuest = false;
                    }
                    else
                    {
                        // un클리어 텍스트
                        UIManager.instance.GetText(questClearUnClear[0].stringList, this.gameObject.GetComponent<NPCController>());
                    }
                    // SendToObserver()를 종료;
                    return;
                }
            }
        }
        else if(questNum < NPCTalk.Count)
        {
            UIManager.instance.GetText(NPCTalk[questNum].stringList, this.gameObject.GetComponent<NPCController>(), NPCTalk[questNum].isQuest);
        }
        else
        {
            m_NPC = NPCType.CanTalk;
            UIManager.instance.GetText(storyStringList, this.gameObject.GetComponent<NPCController>());
            questNum = NPCTalk.Count;
        }
    }
    
}
