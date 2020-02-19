using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAction : MonoBehaviour
{
    public enum KeyType
    {
        UI,Skill
    }

    public KeyType m_KeyType;

    public Skill m_Skill;

    public GameObject target;

    private void Start()
    {
        if (m_KeyType == KeyType.Skill)
        {
            m_Skill = GetComponent<Skill>();
        }
    }


    public void DoAction()
    {
        if (m_Skill != null)
        {
            //스킬실행
            Debug.Log("이것은 스킬입니다.");
        }
        else
        {
            target.SetActive(!target.activeSelf);
        }
    }

}
