using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CityScenePlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    Rigidbody rigid;
    Animator anim;
    //MoveObstacle obstacle;
    
    public ParticleSystem jumpPs;
    public ParticleSystem DiePs;
    public float jumpPower;
    bool isJump;
    public float hAxis;
    public float Speed;
    public bool isfallingFruits;
    public bool ishurdleUp;

    bool isDie;
    // Start is called before the first frame update
    void Start()
    {
        anim = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        DiePs.gameObject.SetActive(false);
       
    }

    void Update()
    {

        Jump();

    }
    void FixedUpdate()
    {
        if (!isDie)
        {
            GetInput();
            Move();
        }
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
       
    }
    void Move()
    {
        
        Vector3 position = transform.position;
        position.x += hAxis * Time.smoothDeltaTime * Speed;
        position.z += Time.smoothDeltaTime * Speed;
        transform.position = position;
        anim.SetTrigger("doRun");
        anim.SetBool("isRun", true);
        
    }
    void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpPs.Play();
            }

        }
       
    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            
            isJump = false;
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            TagisObj();
        }

    }
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Obstacle")
        {
            TagisObj();
        }    
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DropBox")
        {
            isfallingFruits = true;

        }
        if (other.tag == "Obstacle")
        {
            TagisObj();
        }
        if(other.tag == "Hurdle")
        {
            ishurdleUp = true;
            Invoke("hurdleDownSet", 2.5f);
        }
    }
    void hurdleDownSet()
    {
        ishurdleUp = false;
    }
    void TagisObj()
    {
        isDie = true;
        DiePs.gameObject.SetActive(true);
        anim.SetTrigger("doDie");
        anim.SetBool("isRun", false);

        Invoke("ReLoadScene", 1.5f);
    }
    void ReLoadScene()
    {
        SceneManager.LoadScene("CityScene");
    }
}
