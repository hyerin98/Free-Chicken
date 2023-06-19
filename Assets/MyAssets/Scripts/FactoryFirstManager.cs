using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryFirstManager : MonoBehaviour
{
    public Animator anim;
    public GameObject[] boxPos;
    public bool isContact;
    public GameObject particle;
    //public GameObject Dieparticle;
    int box;
    public FactoryPlayer player;
    //public PlayerChangeEgg playeregg;

    public GameObject talkCanvas1;
    public GameObject talkCanvas2;
    public GameObject npcCam;
    public GameObject mainCam;

    public GameObject eggBox;
    public bool isChk;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        //playeregg = GameObject.Find("PlayerEgg").GetComponent<PlayerChangeEgg>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isContact)
        {
            StartCoroutine(Spawn());
            
        }
    }
    IEnumerator Spawn()
    {
        isContact = false;
        Vector3 pos = GetRandomPos();
        GameObject instance = Instantiate(particle,pos,Quaternion.identity);
        if(player.tmpBox.transform.position == pos)
        {
            //GameObject ob = Instantiate(Dieparticle, pos, Quaternion.identity);
            Debug.Log("야야 니 주것댥");
            yield return new WaitForSeconds(3f);
            //Destroy(ob);
            talkCanvas1.SetActive(true);
            Invoke("Die",3f);
        }
        else
        {
            
            Debug.Log("니 살았닭");
            yield return new WaitForSeconds(1.5f);
            talkCanvas2.SetActive(true);
            Invoke("Trun",3f);
           
        }
        Destroy(instance,3f);
    }
    void Die()
    {
        talkCanvas1.SetActive(false);
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
        eggBox.SetActive(false);
        player.Pos();
    }
    void Trun()
    {
        talkCanvas2.SetActive(false);
        npcCam.SetActive(false);
        mainCam.SetActive(true);
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
        eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0.1f;
        Debug.Log("속도 업");
        anim.SetBool("isAttack",false);
        player.isStopSlide = false;

    }
    Vector3 GetRandomPos()
    {
       
        box = Random.Range(0, boxPos.Length);
        
        Vector3 pos = boxPos[box].transform.position;
        return pos;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EggBox" && !isChk)
        {
            Debug.Log("충돌");
            isContact = true;
            isChk = true;
            eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0f;
            Debug.Log("속도다운");
            anim.SetBool("isAttack", true);
        }
        
    }
}
