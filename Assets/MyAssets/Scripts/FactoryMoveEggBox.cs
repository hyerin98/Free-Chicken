using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryMoveEggBox : MonoBehaviour
{
    public float Speed;
    public GameObject mainCam;
    public GameObject moveCam;
    public FactoryPlayer player;
    public bool isChk;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isSetEggFinish && !isChk)
        {
            Check();
        }
       
       
    }
    void Check()
    {

        Speed = 0.1f;
        mainCam.gameObject.SetActive(false);
        moveCam.gameObject.SetActive(true);
        isChk = true;

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slide")
        {

            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        }
        if (other.tag == "TurnPointR")
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
        }
        if (other.tag == "TurnPointL")
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        }
        if (other.tag == "TurnPointD")
        {
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);

        }
    }
    void OnTriggerEnter(Collider other)
    {   
        if(other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        
    }
}
