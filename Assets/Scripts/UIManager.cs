﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject skillUI;
    public List<GameObject> skillSets;
    private KeyCode key;

    private void Start()
    {
    }

    private void Update()
    {
        key = GameManager.instance.key;
        if (key == KeyCode.K)
        {
            Debug.Log(key);
            skillUI.SetActive(!skillUI.activeSelf);
        }

    }



    public void SkillTabClickBtn(int i)
    {
        foreach (GameObject item in skillSets)
        {
            if (item.gameObject == skillSets[i])
            {
                item.SetActive(true);
                continue;
            }
            item.SetActive(false);
        }
        
    }
}
