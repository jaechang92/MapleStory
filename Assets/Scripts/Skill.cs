using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int skillNum;

    public SkillDatabase skillData;

    private void Start()
    {
        skillData = SkillManager.instance.skills[skillNum];
    }

}
