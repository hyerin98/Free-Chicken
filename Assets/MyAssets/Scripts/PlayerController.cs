using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;

    public GameObject player;

    Rigidbody rigid;

    Renderer render;
    SkinnedMeshRenderer skinrender;


    public ParticleSystem damagePs;
    public ParticleSystem jumpPs;
    public bool playDamagePs;
    public bool playJumpPs;


    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;

    public float hAxis;
    public float vAxis;

    bool run;
    bool isJump;
    public float jumpPower = 5f;
    public int jumpCount = 2;   // ����Ƚ��, 2�� 3���� �ٲٸ� 3�� ����

    Vector3 moveDir;

    Animator anim;

    MoveObstacle obstacle;

    public GameObject EggPrefab;
    int eggCnt;

    public float playerHealth;
    public Slider healthbar;

    void Awake()
    {
        anim = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        jumpCount = 0;
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<MoveObstacle>();
        playerHealth = 100f;

        playDamagePs = true;
        playJumpPs = true;

        eggCnt = 10;
        //damagePs.Play();
    }

    void Update()
    {
        LookAround();
        Move();
        GetInput();
        Jump();
        UI();
        StartCoroutine(Fire());
    }
    void UI()
    {
        

        if (healthbar.value == 0)
        {
            player.transform.position = new Vector3(0, 0, 0);
            healthbar.value = 100f;
        }
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector2 moveInput = new Vector2(hAxis, vAxis);
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            obstacle.isPlayerFollow = false; // MovePlatform
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * finalSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        //Debug.DrawRay(cameraArm.position,new Vector3(cameraArm.forward.x,0f,cameraArm.forward.z).normalized,Color.red);
    }

    void Jump()
    {
        if (jumpCount > 0)
        {
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                //isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                --jumpCount;

                jumpPs.Play();
            }
        }
    }

    IEnumerator Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (eggCnt > 0)
            {
                Vector3 firePos = transform.position + anim.transform.forward + new Vector3(0f, 0.8f, 0f);
                var Egg = Instantiate(EggPrefab, firePos, Quaternion.identity).GetComponent<PlayerEgg>();
                Egg.Fire(anim.transform.forward);
                --eggCnt;

                if (eggCnt <= 0)
                {
                    yield return new WaitForSeconds(5f);
                    eggCnt = 10;
                }
            }

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" /*|| collision.gameObject.tag == "Obstacle"*/)
        {
            jumpCount = 2;
            isJump = false;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            // ��ֹ� �浹 �� ƨ����������? ȿ���ֱ�
            //ContactPoint cp = collision.GetContact(0);
            //moveDir = player.transform.position - cp.point;
            //rigid.AddForce((moveDir).normalized * 20f, ForceMode.Impulse);

            /* playerHealth -= 10f;
             healthbar.value -= 10f;*/
            jumpCount = 1;
            isJump = false;
            //damagePs.Play();
        }
      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire" || other.gameObject.tag == "Rock")
        {
            healthbar.value -= 5f;
        }
    }
    private void LookAround() // ī�޶�
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 100f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
