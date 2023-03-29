using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss : MonoBehaviour
{
    Rigidbody rb;
    PlayerController target;
    Animator bossAnim;
    CameraShake cameraShake;
  
    public float moveSpeed;
    public NavMeshAgent nav;
    public bool isChase;

    public bool isAttack;
    public GameObject fireAttack;
    public bool isRockFalling;

    public bool isRandomSpace;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("Character").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        bossAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        Invoke("ChaseStart", 2);
        
    }
    void ChaseStart()
    {
       
        isChase = true;
        
        bossAnim.SetBool("isWalk", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (nav.enabled)
        {
           
            nav.isStopped = !isChase;
            nav.SetDestination(target.transform.position);
            nav.speed = moveSpeed;
        }
       
    }
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        bossAnim.SetBool("isAttack", true);
        int randomRange = Random.Range(0, 5);
        switch (randomRange)
        {
            case 0:
               
                bossAnim.SetTrigger("doAttack1");
                Debug.Log("플레이어 공격! 주먹발사 모션 발싸");
                isRandomSpace = true;
                yield return new WaitForSeconds(2f);
                break;
            case 1:
               
                bossAnim.SetTrigger("doFireAttack");
                Debug.Log("플레이어 공격! doFireAttack 발싸");
                // 불 발싸
                fireAttack.SetActive(true);
                  
                yield return new WaitForSeconds(2f);
                break;
            case 2:
                bossAnim.SetTrigger("doJump");
                // 돌떨어지게 하기
                isRockFalling = true;
                cameraShake.StartCoroutine(cameraShake.Shake(.5f, .3f));
                yield return new WaitForSeconds(3f);
                break;
            case 3:
                bossAnim.SetTrigger("doFireShoot");
                Debug.Log("바닥 내려치기");
                isRockFalling = true;
                cameraShake.StartCoroutine(cameraShake.Shake(.5f, .3f));
                yield return new WaitForSeconds(3f);
                break;

           // case 4:

        }
        isAttack = false;
        fireAttack.SetActive(false);
        bossAnim.SetBool("isAttack", false);
        isChase = true;
        
    }
  
    void FixedUpdate()
    {
        
        FreezeVelocity();
        Targeting();

    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    void Targeting()
    {
       
        float targetRadius = 1.5f;
        float targetRange = 3f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }
}                               
  