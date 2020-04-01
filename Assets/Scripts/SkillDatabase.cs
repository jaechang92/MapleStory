using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SkillDatabase
{

    public string skillName;
    public Sprite skillImage;
    public enum skillType { Ative,Passive };
    public skillType m_skillType;
    public float skillPersentDamage;
    public float skillDelayTime;
    public Vector2 skillRange = new Vector2();
    public float usedMana;
    public GameObject skillEffect;
    public bool isMulti;


}
