using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _animator;
    Camera _camera;
    CharacterController _controller;

    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;

    public float jumpSpeed = 4.0f;
    public float gravity = 9.81f;

    public bool toggleCameraRotation;
    public bool run;

    Vector3 moveDirection = Vector3.zero;

    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        _controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();
        InputMovement();
        //Jump();

        //if (_controller.isGrounded)
        //{
        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    moveDirection = transform.TransformDirection(moveDirection);
        //    moveDirection *= speed;
        //    if (Input.GetButton("Jump"))
        //        moveDirection.y = jumpSpeed;

        //}
        //moveDirection.y -= gravity * Time.deltaTime;
        //_controller.Move(moveDirection * Time.deltaTime);


        //moveDirection.y -= gravity * Time.deltaTime;
    }

    void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime);
        }
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }
    }

    void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;

        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;



        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);


        //finalSpeed = (run) ? runSpeed : speed;

        //    Vector3 forward = transform.TransformDirection(Vector3.forward);
        //    Vector3 right = transform.TransformDirection(Vector3.right);

        //    moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        //    _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);


        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

    }

    //void Jump()
    //{

    //        if (Input.GetButton("Jump"))
    //            moveDirection.y = jumpSpeed;

    //}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class Player : MonoBehaviour
//{
//    Animator _animator;
//    Camera _camera;
//    CharacterController _controller;

//    public float speed = 5f;
//    public float runSpeed = 8f;
//    public float finalSpeed;

//    public bool toggleCameraRotation;
//    public bool run;
//    public float gravity;

//    public float smoothness = 10f;

//    //public float rspeed;
//    //float hAxis;
//    //float vAxis;
//    //bool rDown;

//    bool isJump;
//    public float jumpSpeed = 5f; // 점프 속도
//    public bool isGrounded = false;
//    public int jumpCount = 2;   // 점프횟수, 2를 3으로 바꾸면 3단 점프

//    //Vector3 moveVec;

//    //Rigidbody rigid;
//    //Animator anim;

//    void Awake()
//    {
//        //_camera = Camera.main;
//        //rigid = GetComponent<Rigidbody>();
//        //anim = GetComponentInChildren<Animator>();
//        jumpCount= 0;
//    }

//    void Start()
//    {
//        _animator = this.GetComponent<Animator>();
//        _camera = Camera.main;
//        _controller = this.GetComponent<CharacterController>();
//        gravity = 20.0f;
//    }

//    void Update()
//    {
//        ToggleCamera();
//        GetInput();
//        Move();
//        //Turn();
//        //Jump();


//    }

//    void LateUpdate()
//    {
//        if (toggleCameraRotation != true)
//        {
//            Vector3 playerRoatate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
//            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRoatate), Time.deltaTime * smoothness);
//        }
//    }

//    void ToggleCamera()
//    {
//        if (Input.GetKey(KeyCode.LeftAlt))
//            toggleCameraRotation = true;
//        else
//            toggleCameraRotation = false;
//    }

//    void GetInput()
//    {
//        if (Input.GetKey(KeyCode.LeftAlt))
//        {
//            toggleCameraRotation = true;
//        }
//        else
//        {
//            toggleCameraRotation = false;
//        }
//        if (Input.GetKey(KeyCode.LeftShift))
//        {
//            run = true;
//        }
//        else
//        {
//            run = false;
//        }

//        //hAxis = Input.GetAxisRaw("Horizontal");
//        //vAxis = Input.GetAxisRaw("Vertical");
//        //rDown = Input.GetButton("Run");
//        isJump = Input.GetButtonDown("Jump");
//    }

//    void Move()
//    {

//        finalSpeed = (run) ? runSpeed : speed;

//        Vector3 forward = transform.TransformDirection(Vector3.forward);
//        Vector3 right = transform.TransformDirection(Vector3.right);

//        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");


//        _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);



//        if (_controller.isGrounded)
//        {
//            if (jumpCount > 0)
//            {
//                if (isJump)
//                {
//                    moveDirection.y = jumpSpeed;
//                    //rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
//                    --jumpCount;

//                }
//            }
//        }

//        moveDirection.y -= gravity * Time.deltaTime;


//        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
//        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

//        //moveVec = new Vector3(hAxis, 0, vAxis).normalized;

//        //transform.position += moveVec * rspeed * (rDown ? 2f : 1f) * Time.deltaTime;

//        //anim.SetBool("Walk", moveVec != Vector3.zero);
//        //anim.SetBool("Run", rDown);
//    }

//    //void Turn()
//    //{
//    //    transform.LookAt(transform.position + moveVec);
//    //}

//    //void Jump()
//    //{
//    //    //if(_controller.isGrounded)
//    //    //{
//    //    //    if(jumpCount > 0)
//    //    //    {
//    //    //        if(isJump)
//    //    //        {
//    //    //            moveDirection.y = jumpSpeed;
//    //    //            //rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
//    //    //            --jumpCount;

//    //    //        }
//    //    //    }
//    //    //}
//    //    //if (isJump)
//    //    //{
//    //    //    rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
//    //    //}
//    //}

//    //void OnCollisionEnter(Collision collision)
//    //{
//    //    if(collision.gameObject.tag == "Floor")
//    //    {
//    //        isGrounded= true;   // Floor에 닿으면 isGround는 true
//    //        jumpCount = 2;      // Floor에 닿으면 점프횟수가 2로 초기화됨
//    //        isJump = false;
//    //    }
//    //}

//    void OnControllerColliderHit(ControllerColliderHit hit)
//    {
//        if(hit.collider.gameObject.tag == "Floor")
//        {
//            isGrounded = true;   // Floor에 닿으면 isGround는 true
//            jumpCount = 2;      // Floor에 닿으면 점프횟수가 2로 초기화됨
//            isJump = false;
//        }

//    }
//}
