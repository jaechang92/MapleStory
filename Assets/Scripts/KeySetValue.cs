﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeySetValue : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.name);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        UIManager.instance.m_NowObject = this.gameObject;
        Debug.Log(gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.m_NowObject = null;
    }




    private Image m_sprite;
    private Sprite initSprite = null;

    public enum MyType
    {
        Skill,UI
    }

    public MyType m_type;

    public KeyAction m_KeyAction;

    void Start()
    {
        m_sprite = this.GetComponent<Image>();
    }

    public void GetInit(GameObject obj)
    {
        //if (obj.CompareTag("Skill"))
        //{
        //    GetImage(obj.GetComponent<Skill>().skillNum);
        //}
        //else if(obj.CompareTag("NotSkillUI"))
        //{
        //    //스킬 UI가 아닐때 인벤토리 온오프 하도록 만드는 기능
        //}

        ContainerInKeySets(obj.GetComponent<KeyAction>());
        m_KeyAction = obj.GetComponent<KeyAction>();
        m_sprite.sprite = obj.GetComponent<Image>().sprite;
        if (m_KeyAction.m_KeyType == KeyAction.KeyType.UI)
        {
            m_type = MyType.UI;
        }
        else if(m_KeyAction.m_KeyType == KeyAction.KeyType.Skill)
        {
            m_type = MyType.Skill;
        }

    }

    public void ContainerInKeySets(KeyAction KeyAtionScript)
    {
        foreach (var item in UIManager.instance.keySets)
        {
            if (item.m_KeyAction != null)
            {
                Debug.Log("??");
                Debug.Log(item.m_KeyAction == KeyAtionScript);
                Debug.Log(item.m_KeyAction);
                Debug.Log(KeyAtionScript);
                if (item.m_KeyAction == KeyAtionScript)
                {
                    item.init();
                }
            }
        }
        

    }

    //public void GetImage(int num)
    //{
    //    foreach (var item in UIManager.instance.keySets)
    //    {
    //        if (item.skillNum == num)
    //        {
    //            item.init();
    //        }
    //    }

    //    //skillNum = num;
    //    //skill_sprite = SkillManager.instance.skills[skillNum].skillImage;
    //    //Debug.Log("Get");
    //    //m_sprite.sprite = skill_sprite;
    //}

    private void init()
    {
        m_sprite.sprite = initSprite;
        //skillNum = -1;
        m_KeyAction = null;
        Debug.Log("초기화");
    }
}
