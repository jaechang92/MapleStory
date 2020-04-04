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


    public void DoAction(GameObject player)
    {
        if (m_Skill != null)
        {
            player.GetComponent<CharacterController2D>().IsSkillAtive = true;
            //스킬실행
            Debug.Log("이것은 스킬입니다.");
            Debug.Log("이스킬의 마나 = " + m_Skill.skillData.usedMana);
            Debug.Log("이스킬의 이름 = " + m_Skill.skillData.skillName);

            // 이펙트 생성 
            GameObject skillEffect = m_Skill.skillData.skillEffect;
            skillEffect.transform.parent = player.transform;
            
            // 애니메이션 실행
            skillEffect.SetActive(true);


            
            //Instantiate(m_Skill.skillData.skillEffect, player.transform);


        }
        else
        {
            target.SetActive(!target.activeSelf);
        }
    }

    public bool IsMoveCantroller()
    {
        if (m_Skill != null)
        {
            return m_Skill.skillData.canMove;
        }
        else
        {
            return true;
        }
    }

    
}
