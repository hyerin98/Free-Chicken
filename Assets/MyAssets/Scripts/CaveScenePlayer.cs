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
    //public int jumpCount = 2;   // ����Ƚ��, 2�� 3���� �ٲٸ� 3�� ����

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
            Debug.Log("�÷��̾� ��ֹ� �浹����. ���!");
            anim.SetTrigger("isDead");
            Invoke("Restart", 2.5f);
        }
    }

    void Restart()
    {
        transform.position = new Vector3(0, 0, 0); // �÷��̾� ��ֹ� �浹�� ������
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sense")
        {
            Debug.Log("�÷��̾� �������� �� ������");
            isSense = true;
            // �÷��̾ ��ֹ����� Ʈ���Ŷ� ���˵Ǹ�, ��ֹ� �׶� �ٷ� �����̰Բ�
            //ObstacleTestobstacleTest.transform.position += new Vector3(0, 0, -1) * 1f * Time.deltaTime;
            //ObstacleTestobstacleTest.transform.Rotate(0, 0, -50 / 50);
        }


        if(other.gameObject.tag == "SenseTest")
        {
            Debug.Log("�÷��̾� ���ӻ�");
            isSenseTest = true;
        }

        if(other.tag == "Key")
        {
            ++keyCount;
            other.gameObject.SetActive(false);
            //manager.GetKey(keyCount);
            Debug.Log("Ű�� �����!");
        }
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // LookAt(): ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
    }

}
