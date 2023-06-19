using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CitySceneToCaveScene : MonoBehaviour
{
    
    public GameObject pos;
    public CityScenePlayer player;
    public bool isContact;
    public bool isMove;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CityCharacter").GetComponent<CityScenePlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isLast)
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 3f, Space.World);
            if (isContact)
            {
                player.gameObject.transform.position = pos.transform.position;
            }
        }

    }
  
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isContact = true;
        }
    }
}
