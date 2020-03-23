using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{

    public enum NPCType
    {
        Normal,CanQuest ,CanTalk
    }

    public NPCType m_NPC;
    
    
    [System.Serializable]
    public struct TextList
    {
        public List<string> stringList;
    }


    public List<TextList> textLists;


    public int questNum = 0;

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

                if (questNum < textLists.Count)
                {

                UIManager.instance.GetText(textLists[questNum].stringList,this.gameObject.GetComponent<NPCController>());
                }

                //questNum++;

                break;
            case NPCType.CanTalk:
                Debug.Log("스토리 이야기");
                break;
            default:
                break;
        }
    }

    
}
