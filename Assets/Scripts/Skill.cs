using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int skillNum;

    public SkillDatabase skillData;

    private void Awake()
    {
        skillData = SkillManager.instance.skills[skillNum];
        
    }


    private void Start()
    {
    }

}
