using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySetValue : MonoBehaviour
{
    public KeyCode keyValue;

    public int skillNum;
    public Sprite skill_sprite;

    private Image m_sprite;
    private Sprite initSprite = null;
    void Start()
    {
        m_sprite = this.GetComponent<Image>();
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
