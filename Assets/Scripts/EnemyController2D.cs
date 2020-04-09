using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidbody2D;

    private GameManager.MapEnum MapEnum;
    [SerializeField]
    private LayerMask targetMask;
    private void Awake()
    {
        m_spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log(GameManager.instance);
        MapEnum = GameManager.instance.m_Mapname;
    }

    private void OnEnable()
    {
        CreateSystem.instance.OnEnableObject(this.gameObject);
        switch (MapEnum)
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


    public enum EnemyState
    {
        NomalMove, Tracking , Attack , ATKMode
    }


    public EnemyState state = EnemyState.NomalMove;
    public float moveSpeed;

    /// <summary>
    /// *   x = min 
    ///     y = max
    /// </summary>
    public Vector2 rTime;

    [System.Serializable]
    public struct EnemyInfo
    {
        public int ID;
        public int hp;
        public float trackingRange;
        public int attackDamage;
        public int attackRange;
        public int defens;
        public int exp;

        public float attackDelay;
    }

    public EnemyInfo m_EnemyInfo;


    public bool isGrounded = true;
    public bool m_FacingRight = false;  // For determining which way the player is currently facing.
    private float currentTime = 0.0f;
    private float randomTime = 0;

    private float currentAttackDelay;


    private Transform myTr;
    public Transform targetTr;
    private bool trackingOnOff;
    
    [SerializeField]
    private Transform m_GroundCheck;
    [SerializeField]
    private float jumpPower;
    private bool isJumped;

    // Start is called before the first frame update
    void Start()
    {
        myTr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    
    // 쉬프트 연산 1 << 밀어낸다 
    // 1<< 1 = 10  / 1 << 2 = 100
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, (1 << 9) + (1 << 10));

        for (int i = 0; i < colliders.Length; i++)
        {
            isGrounded = true;
        }

        //isGrounded


        //Debug.DrawRay(Vector2.up * 0.5f + transform.position.y * Vector2.up + Vector2.left * m_EnemyInfo.attackRange + transform.position.x * Vector2.right, Vector2.right * m_EnemyInfo.attackRange,Color.red);
        //Move();
        FSM2();

    }


    private void Move()
    {
        m_rigidbody2D.velocity = new Vector2(moveSpeed, m_rigidbody2D.velocity.y);
        if (isGrounded == true && isJumped == true)
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, jumpPower);
            isGrounded = false;
        }

    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        
    }

    // random time1~time2 까지
    public void RandomForTime(float time1, float time2)
    {
        if (time1 < time2)
        {
            float temp = time1;
            time1 = time2;
            time2 = temp;
        }
        randomTime = Random.Range(time1, time2);
        
    }

    private void Attack()
    {
        GameManager.instance.m_CharacterController2D.Hit(m_EnemyInfo.attackDamage);
        currentAttackDelay = m_EnemyInfo.attackDelay;
        moveSpeed = 0;
        Debug.Log("Attack");
    }

    private void Attacked(int damage)
    {
        state = EnemyState.ATKMode;
        m_EnemyInfo.hp -= damage;
        Debug.Log("데미지");
        if (m_EnemyInfo.hp <= 0)
        {
            IsDie();
        }
    }

    private void IsDie()
    {
        Debug.Log("죽음");
        EventManager.instance.QuestObserver(m_EnemyInfo);


        this.gameObject.SetActive(false);
    }
    
    private void FSM()
    {
        currentAttackDelay -= Time.deltaTime;

        if (Mathf.Abs(myTr.position.x - targetTr.position.x) <= m_EnemyInfo.attackRange)
        {
            state = EnemyState.Attack;

            if (currentAttackDelay < 0)
            {
                Attack();
            }


        }
        else if (Mathf.Abs(myTr.position.x - targetTr.position.x) <= m_EnemyInfo.trackingRange)
        {
            state = EnemyState.Tracking;
        }
        else
        {
            state = EnemyState.NomalMove;
        }


        // m_FacingRight 가 참이면 moveSpeed 는 양수 거짓이면 음수
        moveSpeed = m_FacingRight ? 1 : -1;
        if (EnemyState.NomalMove == state)
        {
            currentTime += Time.deltaTime;
            if (currentTime > randomTime)
            {
                currentTime = 0;
                RandomForTime(rTime.x, rTime.y);
                Flip();
            }
        }
        else if (EnemyState.Tracking == state)
        {
            if (myTr.position.x - targetTr.position.x < 0 && !m_FacingRight
                || myTr.position.x - targetTr.position.x > 0 && m_FacingRight)
            {
                Flip();
            }
        }
        else if (EnemyState.Attack == state)
        {
            moveSpeed = 0;
            if (myTr.position.x - targetTr.position.x < 0 && !m_FacingRight
                || myTr.position.x - targetTr.position.x > 0 && m_FacingRight)
            {
                Flip();
            }
        }
    }


    private void FSM2()
    {
        if (state == EnemyState.ATKMode)
        {
            Vector2 dir = myTr.position - targetTr.position;
            float distance = Vector2.Distance(myTr.position, targetTr.position);
            if ((dir.x < 0 && myTr.localScale.x > 0) ||
                    (dir.x > 0 && myTr.localScale.x < 0))
            {
                Flip();
            }

            if (distance > m_EnemyInfo.trackingRange)
            {
                moveSpeed = -myTr.localScale.x;
                isJumped = true;

            }
            else if (distance < m_EnemyInfo.trackingRange)
            {
                isJumped = false;
                Attack();
                
            }
        }
        else if(state == EnemyState.NomalMove)
        {
            currentTime += Time.deltaTime;
            if (currentTime > randomTime)
            {
                currentTime = 0;
                RandomForTime(rTime.x, rTime.y);
                Flip();

                int rBool = Random.Range(0, 2);
                if (rBool == 0)
                {
                    //idle
                    moveSpeed = 0;
                    isJumped = false;
                }
                else
                {
                    moveSpeed = -myTr.localScale.x;
                    isJumped = true;
                }
            }
        }
        Move();
    }


    private void init()
    {

    }
    
}
