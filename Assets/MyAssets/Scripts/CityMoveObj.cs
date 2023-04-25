using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMoveObj : MonoBehaviour
{
    public enum MoveObstacleType { A, B};
    public MoveObstacleType Type;
    CityScenePlayer player;
    float initPositionY;
    float initPositionX;
    public float distance;
    public float turningPoint;

    public bool turnSwitch;
    public float moveSpeed;

    //MovePlatform
    public bool isMove;
    public bool isPlayerFollow;

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CityCharacter").GetComponent<CityScenePlayer>();
        isPlayerFollow = false;
    }
    void Awake()
    {
        if (Type == MoveObstacleType.B) // Right & Left
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;

        }
    }
    void rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (isPlayerFollow)
        {
            player.gameObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

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
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
      
        if (collision.gameObject.tag == "Player" && isMove)
        {
            isPlayerFollow = true;

        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (Type)
        {
            case MoveObstacleType.A:
                isMove = true;
                //isPlayerAttack = true;
                rotate();

                break;
            case MoveObstacleType.B:
                isMove = true;
                
                leftRight();

                break;
        }
    }
}
