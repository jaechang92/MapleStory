using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EventManager : MonoBehaviour
{
    static public EventManager instance;

    public delegate void QuestUpdate();

    public event QuestUpdate questUpdateEvnet;
    public event QuestUpdate talkEvent;

    public List<QuestSet> quests;

    [System.Serializable]
    public struct QuestSet
    {
        public int nowNPC;

        public int questMonsterID;
        public int killCount;

        public reward rewards;
        public bool isClear;
        public int count
        {
            get
            {
                return killCount;
            }

            set
            {
                if (value <= 0)
                {
                    killCount = 0;
                    isClear = true;
                }
                else
                {
                    killCount = value;
                }
                Debug.Log(killCount);
            }
            
        }


        public QuestSet(int _nowNPC, int _ID, int _countNum, reward _reward, int _count, bool _isClear)
        {
            nowNPC = _nowNPC;
            questMonsterID = _ID;
            killCount = _countNum;
            Debug.Log(_countNum);
            rewards = _reward;
            isClear = _isClear;
        }



        //public QuestSet DeepCopy()
        //{
        //    QuestSet newCopy = new QuestSet(questMonsterID, killCount, rewards, count, isClear);
        //    newCopy.questMonsterID = this.questMonsterID;
        //    newCopy.killCount = this.killCount;
        //    newCopy.rewards = this.rewards;
        //    newCopy.count = this.count;
        //    newCopy.isClear = this.isClear;
        //    return newCopy;
        //}
    }

    [System.Serializable]
    public struct reward
    {
        public int Exp;
        // 아이템
        // 등등.....
    }

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
    
    public int Added(int NPC,int ID,int Count, reward Reward)
    {
        QuestSet temp = new QuestSet(NPC, ID,Count, Reward, 0, false);
        
        quests.Add(temp);

        return quests.Count - 1;
    }

    public void RemoveQuestList(QuestSet quest)
    {
        quests.Remove(quest);
    }

    public void QuestObserver(EnemyController2D.EnemyInfo info)
    {
        //foreach (var item in quests)
        //{
        //    if (item.questMonsterID == info.ID)
        //    {
        //        QuestSet temp = item;
        //        temp.DeepCopy();
        //        item = temp;
        //    }
        //}

        //for (int i = 0; i < quests.Count; i++)
        //{
        //    if (quests[i].questMonsterID == info.ID)
        //    {
        //        QuestTest temp = quests[i].QT;
        //        temp.Count++;
        //        quests[i].QT = temp;
        //    }
        //}
    }

    public void OnQuestEvent()
    {
        questUpdateEvnet();
    }

    public void OnTalkEvent()
    {
        talkEvent();
    }
}
