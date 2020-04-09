using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    static public EventManager instance;
    


    public List<QuestSet> quests;

    [System.Serializable]
    public struct QuestSet
    {


        public int questMonsterID;
        public int killCount;

        public reward rewards;

        public int count { get; set; }


        public QuestSet(int _ID, int _countNum, reward _reward, int _count)
        {
            questMonsterID = _ID;
            killCount = _countNum;
            rewards = _reward;
            count = _count;
        }

        

        public QuestSet DeepCopy()
        {
            QuestSet newCopy = new QuestSet(questMonsterID, killCount, rewards, count);
            newCopy.questMonsterID = this.questMonsterID;
            newCopy.killCount = this.killCount;
            newCopy.rewards = this.rewards;
            newCopy.count = this.count;
            return newCopy;
        }
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
    
    public void Added(int ID,int Count, reward Reward)
    {
        QuestSet temp = new QuestSet(ID,Count, Reward, 0);
        
        quests.Add(temp);
    }

    public void QuestObserver(EnemyController2D.EnemyInfo info)
    {
        foreach (var item in quests)
        {
            if (item.questMonsterID == info.ID)
            {
                QuestSet temp = item;
                temp.DeepCopy();
                item = temp;
            }
        }

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].questMonsterID == info.ID)
            {
                QuestTest temp = quests[i].QT;
                temp.Count++;
                quests[i].QT = temp;
            }
        }
    }

}
