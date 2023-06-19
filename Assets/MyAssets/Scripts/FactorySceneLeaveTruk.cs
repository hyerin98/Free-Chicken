using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactorySceneLeaveTruk : MonoBehaviour
{
    public bool isTouch;
    public GameObject particle;
    public GameObject showCanvas;
    public bool isScene2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);
            showCanvas.SetActive(true);
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�浹");
            isTouch = true;
            particle.SetActive(true);

            Invoke("ReLoad", 5f);
        }
    }
    void ReLoad()
    {
        if (isScene2)
        {
            SceneManager.LoadScene("FactoryScene_2");
        }
       
    }
}
// �� ������ 
// �ι��� �� �����Ҷ� ���� ȭ�� 3�� �Ŀ� �ٽ� ���� 
