using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FactoryPlayer_2 : MonoBehaviour
{
    //public GameObject thisMesh;
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

    //public Slider EbuttoSlider;


    public bool isTalk;

 
    public bool isStopSlide;
   
    public bool isContact;

 
    public GameObject DieCanvas;
    public GameObject scene2LastUI;
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        //fixUI = gameObject.GetComponent<FactoryFixUI>();
    }
   
    void Update()
    {

        if (!isTalk && !isDie)
        {
            Move();
            GetInput();
            Turn();
            Jump();
            
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
        transform.LookAt(transform.position + moveVec); // LookAt(): ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
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
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox")
        {

            isJump = false;
        }
        if (collision.gameObject.tag == "ObstacleZone2")
        {
            isDie = true;
            anim.SetTrigger("doDie");
            DieCanvas.SetActive(true);

            Invoke("ExitCanvas", 2.5f);
        }

    }
    void ExitCanvas()
    {
        DieCanvas.gameObject.SetActive(false);
        isDie = false;
        SceneManager.LoadScene("FactoryScene_2");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Rail")
        {
            scene2LastUI.gameObject.SetActive(true);
            Invoke("RoadScene", 5f);
        }
    }
    void RoadScene()
    {
        scene2LastUI.gameObject.SetActive(false);
        SceneManager.LoadScene("FactoryScene_3");
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slide")
        {
            
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 1f, Space.World);
           
            
            // �̵��ϴ� ���� �Ĵٺ��� ����
        }
        if (other.tag == "TurnPointR")
        {

            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);
           
        }
        if (other.tag == "TurnPointL")
        {

            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);
            
        }
        if (other.tag == "TurnPointD")
        {

            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * 1f, Space.World);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Slide")
        {
            isSlide = false;
        }
       
        //speed = 2.5f;
    }


}
