using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CaveScenePlayer : MonoBehaviour
{
    //[SerializeField] private Transform characterBody;
    //[SerializeField] private Transform cameraArm;

    Vector3 moveVec;
    bool wDown;
    bool Dead;

    Rigidbody rigid;

    public bool isSense;
    public bool isSenseTest;

    //public ParticleSystem jumpPs;
    //public bool playJumpPs;

    public float speed;

    public float hAxis;
    public float vAxis;

    //bool isJump;
    //public float jumpPower = 5f;
    //public int jumpCount = 2;   // 점프횟수, 2를 3으로 바꾸면 3단 점프

    Animator anim;
    MoveObstacle obstacle;
    GameManager_Cave manager;
    //ObstacleTest ObstacleTestobstacleTest;

    public int keyCount;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<MoveObstacle>();
        //ObstacleTestobstacleTest = GameObject.FindGameObjectWithTag("e").GetComponent<ObstacleTest>();
        //playJumpPs = true;
    }

    void Update()
    {
        Move();
        GetInput();
        Turn();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;  

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("플레이어 장애물 충돌했음. 사망!");
            anim.SetTrigger("isDead");
            Invoke("Restart", 2.5f);
        }
    }

    void Restart()
    {
        transform.position = new Vector3(0, 0, 0); // 플레이어 장애물 충돌시 리스폰
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sense")
        {
            Debug.Log("플레이어 일정범위 안 들어왔음");
            isSense = true;
            // 플레이어가 장애물센서 트리거랑 접촉되면, 장애물 그때 바로 움직이게끔
            //ObstacleTestobstacleTest.transform.position += new Vector3(0, 0, -1) * 1f * Time.deltaTime;
            //ObstacleTestobstacleTest.transform.Rotate(0, 0, -50 / 50);
        }


        if(other.gameObject.tag == "SenseTest")
        {
            Debug.Log("플레이어 들어왓삼");
            isSenseTest = true;
        }

        if(other.tag == "Key")
        {
            ++keyCount;
            other.gameObject.SetActive(false);
            //manager.GetKey(keyCount);
            Debug.Log("키를 얻었닭!");
        }
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // LookAt(): 지정된 벡터를 향해서 회전시켜주는 함수
    }

}
