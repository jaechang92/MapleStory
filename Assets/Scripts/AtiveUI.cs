﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiveUI : MonoBehaviour
{
    public Canvas m_Canvas;
    private void Awake()
    {
        m_Canvas = this.gameObject.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        if (m_Canvas == null)
        {
            m_Canvas = this.gameObject.GetComponent<Canvas>();
        }
        else
        {

            UIManager.instance.m_AtiveInvenSortingList.Add(m_Canvas);
            UIManager.instance.InvenSorting(m_Canvas);
        }
    }

    private void OnDisable()
    {
        UIManager.instance.m_AtiveInvenSortingList.Remove(m_Canvas);
    }
}
