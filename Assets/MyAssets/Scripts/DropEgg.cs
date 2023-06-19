using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DropEgg : MonoBehaviour
{
    public FactoryPlayer factoryPlayer;
    public GameObject getEggCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        factoryPlayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        getEggCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            Debug.Log("알을 얻음");
            //factoryPlayer.anim.SetTrigger("doGetEgg");
            getEggCanvas.gameObject.SetActive(true);
            Destroy(this.gameObject);
            // text 알을 얻었닭 ! 
            // 알 피해서 내려오기
            // 

        }    

    }
}
