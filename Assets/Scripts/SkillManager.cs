using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static public SkillManager instance;
    public List<SkillDatabase> skills;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    

}
