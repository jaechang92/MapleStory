using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{


    public void EndAnimation()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.m_CharacterController2D.IsSkillAtive = false;

        GameManager.instance.m_CharacterController2D.HitCheck();

    }
    
}
