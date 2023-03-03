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
    bool isJump;

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
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        rDown = Input.GetButton("Run");
        isJump = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * rspeed * (rDown ? 1.5f : 1f) * Time.deltaTime;

        
        anim.SetBool("Walk", moveVec != Vector3.zero);
        anim.SetBool("Run", rDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }
    void Jump()
    {
        if (isJump)
        {
            rigid.AddForce(Vector3.up * 3,ForceMode.Impulse);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
