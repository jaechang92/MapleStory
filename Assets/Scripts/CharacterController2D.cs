using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] public bool m_Grounded;                                    // Whether or not the player is grounded.
    [SerializeField] private Collider2D m_ModelCollider2D;                       // My Model Collider
    [SerializeField] private float dubleClickDelayTime = 0.5f;
    [SerializeField] private bool isDubleClick = false;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private int m_InitJumpCount = 1;
    private int m_jumpCount;



    private GameObject nowGround;
    public bool IsSkillAtive = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    [System.Serializable]
    public struct PlayerInfo
    {
        public int hp;

        public int stateDamagePoint;

        public int defens;
        public int exp;

        public float nonHitTime;

    }


    public PlayerInfo m_playerInfo;
    private float currentHitTime;
    public bool isHit = false;


    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        spriteRenderer = m_ModelCollider2D.gameObject.GetComponent<SpriteRenderer>();

    }
    
    private float currentDelayTime;
    private void Update()
    {
        currentHitTime -= Time.deltaTime;
        if (currentHitTime < 0)
        {
            isHit = true;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (isDubleClick)
            {
                OnClick();
                isDubleClick = false;
                return;
            }

            isDubleClick = true;
        }

        if (isDubleClick)
        {
            currentDelayTime += Time.deltaTime;
            if (currentDelayTime >= dubleClickDelayTime)
            {
                currentDelayTime = 0;
                isDubleClick = false;
            }
        }




    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                m_jumpCount = m_InitJumpCount;
                m_Grounded = true;
                if (nowGround != colliders[i].gameObject)
                {
                    nowGround = colliders[i].gameObject;

                }

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Move(float moveH, float moveV, bool jump)
    {

        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(moveH * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (moveH > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (moveH < 0 && m_FacingRight)
            {
                Flip();
            }


        }


        if (m_Grounded && jump && moveV < 0 && nowGround.layer == 10)
        {
            m_ModelCollider2D.enabled = false;

        }
        else if (m_Grounded && jump)
        {
            m_Grounded = false;
            jump = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_jumpCount--;
        }

        if (!m_Grounded && jump)
        {
            if (m_jumpCount > 0)
            {
                m_jumpCount--;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
    }


    public void PressKey(int key)
    {

        Action(key);

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void Action(int i)
    {
        // 키보드의 i번째에 연결된 스킬을 동작한다.
        // if(키보드[i].Mp <= 캐릭터Info.Mp)
        // {
        //      Skill.Ative;
        // }

        if (UIManager.instance.keySets[i].m_KeyAction != null && IsSkillAtive == false)
        {
            UIManager.instance.keySets[i].m_KeyAction.DoAction(this.gameObject);
        }


        //if (UIManager.instance.keySets[i].skillNum >= 0)
        //{
        //    Debug.Log("스킬" + UIManager.instance.keySets[i].skillNum + "번의 마나 =" + SkillManager.instance.skills[UIManager.instance.keySets[i].skillNum].usedMana);
        //}

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == nowGround)
        {
            m_ModelCollider2D.enabled = true;
        }
    }

    public void Hit(int damage)
    {
        if (isHit)
        {
            m_playerInfo.hp -= damage;
            if (m_playerInfo.hp <= 0)
            {
                m_playerInfo.hp = 0;
            }

            StartCoroutine(DoTwinkle());

            currentHitTime = m_playerInfo.nonHitTime;
            isHit = false;

        }
    }





    float persent = -0.1f;
    private IEnumerator DoTwinkle()
    {
        int i = 0;
        while (i < 4)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + persent);
            if (spriteRenderer.color.a >= 1 || spriteRenderer.color.a <= 0.5f)
            {
                persent = -persent;
                i++;
            }
            Debug.Log(spriteRenderer.color.a);
            yield return null;
        }
    }


    public void OnClick()
    {
        
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, 0.1f, 1 << 12);
        

        if (hit)
        {
            hit.transform.GetComponent<NPCController>().Talk();
        }
    }


}
