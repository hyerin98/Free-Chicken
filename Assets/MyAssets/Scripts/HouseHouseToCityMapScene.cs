using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HouseHouseToCityMapScene : MonoBehaviour
{
    public GameObject TalkUI;
    public GameObject CityMapUI;
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("Talk", 3f);
        //Destroy(gameObject,3f);
        
    }
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 3f, Space.World);
    }
    void Talk()
    {
        TalkUI.SetActive(true);
        CityMapUI.SetActive(true);
        
    }
}
