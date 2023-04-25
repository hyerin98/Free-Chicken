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
    public float jumpPower;
    bool isJump;
    public float hAxis;
    public float Speed;
    public bool isfallingFruits;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        
    }

    void Update()
    {

        Jump();

    }
    void FixedUpdate()
    {
        GetInput();
        Move();
        
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
            SceneManager.LoadScene("CityScene");
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
            SceneManager.LoadScene("CityScene");
        }
    }
}
