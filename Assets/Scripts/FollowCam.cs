using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    static private Transform _target = null;
    static public Transform Target
    {
        get
        {
            if (_target == null)
            {
                _target = GameObject.Find("Character").transform;
            }
            return _target;
        }
    }

    private Camera m_Camera;

    public float lerpTime;
    public float damp;
    public Vector2 posOffset;

    public float rightLimit;
    public float leftLimit;
    public float bottomLimit;
    public float topLimit;


    void Start()
    {
        m_Camera = Camera.main;
    }


    void Update()
    {
        Vector3 startPos = this.gameObject.transform.position;
        Vector3 endPos = Target.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, lerpTime * Time.deltaTime);
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x,leftLimit,rightLimit),
                Mathf.Clamp(transform.position.y,bottomLimit,topLimit),
                transform.position.z
            );


        this.gameObject.transform.position = new Vector3(Target.position.x, Target.position.y,-10);
    }
}
