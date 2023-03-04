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

    Camera _camera;
    public bool toggleCameraRotation;
    public float smoothness = 10f;

    Rigidbody rigid;

    Vector3 moveVec;

    Animator anim;

    void Awake()
    {
        _camera = Camera.main;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
            toggleCameraRotation = true;
        else
            toggleCameraRotation = false;

        GetInput();
        Move();
        Turn();
        Jump();
    }

    void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRoatate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRoatate), Time.deltaTime * smoothness);
        }
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
        moveVec = new Vector3(hAxis, 0, vAxis);

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
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
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
