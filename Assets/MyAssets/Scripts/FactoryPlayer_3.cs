using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FactoryPlayer_3 : MonoBehaviour
{
    //public GameObject thisMesh;
    public GameObject startUI;
    public GameObject mainUI;

    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    Vector3 moveVec;
    Rigidbody rigid;

    public bool isJump;
    public float jumpPower;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;

   
    public bool isTalk;

 
    public bool isStopSlide;
    
    public bool isContact;

   
    public ParticleSystem attackParticle;
    public int AttackCnt;
    public GameObject DieCanvas;
    public bool isAttack;

    public GameObject ExitUI;
    public GameObject truckPos;
    public GameObject Truck;
    public Slider OnTruck;
    float t;
    public bool isTruckGo;

    public FactorySceneBomb Bomb;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        isTalk = false;
        
    }
    void Start()
    {
        startUI.SetActive(true);
        mainUI.SetActive(false);
        Invoke("NewStart", 2f);
    }
    void NewStart()
    {
        startUI.SetActive(false);
        mainUI.SetActive(true);
    }
    void Update()
    {

        if (!isTalk && !isDie && !isAttack)
        {
            Move();
            GetInput();
            Turn();
            Jump();
            
        }
        if (isDie)
        {
            Invoke("ExitCanvas", 2.5f);
        }
        if (isTruckGo)
        {
            Truck.transform.Translate(Vector3.forward * Time.deltaTime * 1f);
            // 1초 후에 화면 어두워지기 -> 가정집으로 이동
        }
        
    }
    private void FixedUpdate()
    {
        if (isEbutton)
        {
            if (Input.GetButton("E") && isEbutton)
            {
                if (OnTruck.value < 100f)
                {
                    t += Time.deltaTime;
                    OnTruck.value = Mathf.Lerp(0, 100, t);
                }
                else
                {
                    this.gameObject.transform.position = truckPos.transform.position;

                    ExitUI.gameObject.SetActive(false);
                    
                    Debug.Log("트럭출동");
                    
                    // 검은 화면 3초
                    // 다음씬으로 이어지게
                    isEbutton = false;
                    //.LoadScene("CityScene");
                    isTruckGo = true;
                }

            }

            if (Input.GetButtonUp("E"))
            {
                t = 0;
                OnTruck.value = 0;
            }
        }
        
    }
    // Update is called once per frame
    public void GetInput()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

    }

    void Move()
    {


        if (!(hAxis == 0 && vAxis == 0))
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * speed * Time.deltaTime * 1f;
            anim.SetBool("isWalk", true);


        }
        else if (hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isWalk", false);
        }



    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // LookAt(): 지정된 벡터를 향해서 회전시켜주는 함수
    }
    public void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            Debug.Log("출구");
            ExitUI.gameObject.SetActive(true);
            isEbutton = true;
           
           
        }
        
       
    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox")
        {

            isJump = false;
        }
        if (collision.gameObject.tag == "ObstacleZone3")
        {
            isDie = true;
            anim.SetTrigger("doDie");
            DieCanvas.SetActive(true);

            
        }
        /*if(collision.gameObject.tag == "Obstacle")
        {
            
        }*/
    }
    
    void ExitCanvas()
    {
       
            DieCanvas.gameObject.SetActive(false);
            isDie = false;
            SceneManager.LoadScene("FactoryScene_3");
        
    }

}
