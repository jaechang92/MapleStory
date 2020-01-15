using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float speed;
    public float jumpPower;

    private Rigidbody2D myRigibody2D;
    private Transform myTransform;

    private float h = 0.0f;
    private float v = 0.0f;

    private Vector3 dir;
    private Vector2 dir2;

    private bool isGrounded = false;
    public Vector2 debugVelocity;
    void Start()
    {
        myRigibody2D = this.GetComponent<Rigidbody2D>();
        myTransform = this.GetComponent<Transform>();
        
    }
    
    void FixedUpdate()
    {
        GetKeyDwon();
    }
    
    void Update()
    {
        MyDebug();
    }

    private void MyDebug()
    {
        debugVelocity = myRigibody2D.velocity;
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        dir = Vector3.right * h;
        dir = dir.normalized;
        myTransform.Translate(dir * speed * Time.deltaTime);

    }


    private void GetKeyDwon()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            Move();
        }

        if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.UpArrow))
        {
            Action();
        }


        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            Jump();
        }
    }


    private void Jump()
    {
        myRigibody2D.AddForce(Vector2.up * jumpPower);
    }

    private void Action()
    {

    }

}

