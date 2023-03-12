using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public enum MoveObstacleType { A, B, C, D, E, F };
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
    public bool isDropObj;
    //Swing
   
    public float angle = 0;
    private float lerpTime = 0;
    private float speed = 2f;

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
        if (Type == MoveObstacleType.B) // Right & Left
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;

        }

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
        if(other.gameObject.tag == "Player" && isDropObj)
        {
            
            transform.position = Vector3.Lerp(transform.position, other.transform.position, dropSpeed);
        }
     
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isMove)
        {
            player.healthbar.value -= 10f;
        }
    }
    void OnCollisionStay(Collision collision) 
    {
        if(collision.gameObject.tag == "Player" && isBigJump)
        {
            collision.rigidbody.AddForce(Vector3.up * 30, ForceMode.Impulse);
           
            isBigJump = false;
        }
        else if (collision.gameObject.tag == "Player" && isMove) 
        {
            isPlayerFollow = true;

        }
    }
    void Swing()
    {
        lerpTime += Time.deltaTime * speed;
        transform.rotation = CalculateMovementOfPendulum();


    }
    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * .5f;
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
            case MoveObstacleType.E:
                isDropObj = true;
                break;
            case MoveObstacleType.F:
                Swing();
                break;
        }
       
    }
}
