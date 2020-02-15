﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeySetValue : MonoBehaviour,IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.name);
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        UIManager.instance.m_NowObject = this.gameObject;
    }

    public KeyCode keyValue;

    public int skillNum = -1;
    public Sprite skill_sprite;

    private Image m_sprite;
    private Sprite initSprite = null;

    public enum MyType
    {
        Skill,UI
    }

    public MyType m_type;
    


    void Start()
    {
        m_sprite = this.GetComponent<Image>();
        
    }

    public void GetInit(GameObject obj)
    {
        if (obj.CompareTag("Skill"))
        {
            GetImage(obj.GetComponent<Skill>().skillNum);
        }
        else if(obj.CompareTag("NotSkillUI"))
        {
            //스킬 UI가 아닐때 인벤토리 온오프 하도록 만드는 기능
        }
    }

    public void GetImage(int num)
    {
        foreach (var item in UIManager.instance.keySets)
        {
            if (item.skillNum == num)
            {
                item.init();
            }
        }

        skillNum = num;
        skill_sprite = SkillManager.instance.skills[skillNum].skillImage;
        Debug.Log("Get");
        m_sprite.sprite = skill_sprite;
    }

    private void init()
    {
        m_sprite.sprite = initSprite;
        skillNum = -1;
    }
}
