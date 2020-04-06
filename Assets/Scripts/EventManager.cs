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
    

}
