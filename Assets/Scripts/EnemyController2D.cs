using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    
    private void Awake()
    {
        if (this.gameObject.GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        CreateSystem.instance.OnEnableObject(this.gameObject);
        switch (GameManager.instance.m_Mapname)
        {
            case GameManager.MapEnum.Stage1:
                m_spriteRenderer.sprite = CreateSystem.instance.enemySprites[0];
                break;
        }
        
    }


    private void OnDisable()
    {
        CreateSystem.instance.DisableObject(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
