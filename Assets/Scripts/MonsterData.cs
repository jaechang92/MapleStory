using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterData : MonoBehaviour
{
    static public MonsterData instance;
    public List<MonsterDatabase> monsters;
}

[System.Serializable]
public class MonsterDatabase
{
    
    public int MonsterID;
    public int hp;
    public float trackingRange;
    public int attackDamage;
    public int attackRange;
    public int defens;
    public int exp;

    public float attackDelay;
}
