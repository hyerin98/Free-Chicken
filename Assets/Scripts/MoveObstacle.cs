using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public enum MoveObstacleType { A,B,C,D,E,F};
    public MoveObstacleType Type;
    public PlayerController player;
    
    //UD_Floor
    float initPositionY;
    float initPositionX;
    public float distance;
    public float turningPoint;
    //UD_Floor & LR_Floor
    public bool turnSwitch;
    public float moveSpeed;
   
    //MovePlatform
    public bool isMove; 
    public bool isPlayerFollow;
    
    //RT_Floor
    public float rotateSpeed;

    //Big Jump
    public bool isBigJump;
    //Drop
    public float dropSpeed;
    //Swing
    public float initRotationZ;
    public float swingPoint;
    float z;
    void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerController>();
        isPlayerFollow = false;
    }
    void Awake()
    {
        if (Type == MoveObstacleType.A) // Up & Down
        {
            initPositionY = transform.position.y;
            turningPoint = initPositionY - distance;
            
        }
        if(Type == MoveObstacleType.B) // Right & Left
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;
            
        }

        /*if (Type == MoveObstacleType.F)
        {
            // -180 ~ 0
            initRotationZ = transform.rotation.z; // 현재 z값 == -90
            swingPoint = initRotationZ - distance; // 스윙 포인트는 현재 z값에서 distance 더한 값
        }*/
        // Case C == Rotate
        // Case D == Big Jump

        // Case E == Delay & Drop
        // Case F == Swing
    }

    void upDown()
    {
        float currentPositionY = transform.position.y;

        if (currentPositionY >= initPositionY)
        {
            turnSwitch = false;
        }
        else if (currentPositionY <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            }
        }
    }
    void rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
    void leftRight()
    {

        float currentPositionX = transform.position.x;

        if (currentPositionX >= initPositionX + distance)
        {
            turnSwitch = false;
        }
        else if (currentPositionX <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }

    }
    void OnTriggerEnter(Collider other) // Case E == Delay & Drop
    {
        if(other.gameObject.tag == "Player" )
        {
            
            transform.position = Vector3.Lerp(transform.position, other.transform.position, dropSpeed);
        }
    }
    
    void OnCollisionStay(Collision collision) // 충돌시작시에
    {
        if(collision.gameObject.tag == "Player" && isBigJump)
        {
            collision.rigidbody.AddForce(Vector3.up * 30, ForceMode.Impulse);
           
            isBigJump = false;
        }
        else if (collision.gameObject.tag == "Player" && isMove) // 충돌 중일때 
        {
            isPlayerFollow = true;

        }
    }
    void Swing()
    {


    }
    void Update()
    {
       
        switch (Type)
        {
            case MoveObstacleType.A:
                upDown();
                isMove = true;
                break;
            case MoveObstacleType.B:      
                leftRight();
                isMove = true;

                break;
            case MoveObstacleType.C:
                rotate();
                break;
            case MoveObstacleType.D:
                isBigJump = true;
                break;
            case MoveObstacleType.F:
                Swing();
                break;
        }
       
    }
}
