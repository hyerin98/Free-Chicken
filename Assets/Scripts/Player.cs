using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float rspeed;
    float hAxis;
    float vAxis;
    bool rDown;

    Rigidbody rigid;

    Vector3 moveVec;

    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        rDown = Input.GetButton("Run");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * rspeed * (rDown ? 3f : 1f) * Time.deltaTime;

        
        anim.SetBool("Walk", moveVec != Vector3.zero);
        anim.SetBool("Run", rDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

}
