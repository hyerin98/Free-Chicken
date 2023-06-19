using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FactorySceneChangeZone : MonoBehaviour
{
    public GameObject ChangeConveorZone;
    public GameObject ChangeFinish;
    public Slider ChangeConveorSlider;

    public GameObject zoneL;
    public GameObject zoneR;
    public GameObject zoneG;


    public bool isButton;
    public bool isL;
    public bool isR;
    public bool isG;
    public float t;

    public bool isChk;
    // Start is called before the first frame update
    void Update()
    {
        if (isButton)
        {
            Chk();
        }
    }
    void Chk()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isL && !isG && !isR)
        {
            
            zoneR.gameObject.SetActive(false);
            zoneL.gameObject.SetActive(true);
            isL = true;
            isR = false;
            isG = false;
        }
       
        else if (Input.GetKeyDown(KeyCode.R) && !isG && isL && !isR)
        {
            
            zoneL.gameObject.SetActive(false);
            zoneG.gameObject.SetActive(true);
            isL = false;
            isG = true;
            isR = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.R) && !isR && !isL && isG)
        {
            Debug.Log("��������");
            zoneG.gameObject.SetActive(false);
            zoneR.gameObject.SetActive(true);
            isG = false;
            isR = true;
            isL = false;
            
        } 
        isR = false;
            

        
        if (Input.GetButton("E"))
        {
            Debug.Log("E");
            if (ChangeConveorSlider.value < 100f)
            {
                t += Time.deltaTime;
                ChangeConveorSlider.value = Mathf.Lerp(0, 100, t);
            }
            else // �� ä������
            {


                ChangeConveorZone.gameObject.SetActive(false);
                ChangeFinish.gameObject.SetActive(true);
                isButton = false;
                StartCoroutine(TheEnd());

            }

        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            ChangeConveorSlider.value = 0;
        }

    }
    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(2f);
        ChangeFinish.gameObject.SetActive(false);
        isChk = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isChk)
        {
            isChk = true;
            ChangeConveorZone.SetActive(true);
            isButton = true;
            //StartCoroutine(turnZone());
        }
    }

}
// �ݺ� while
//e ������ ���� 
// �ٽ� ������ ���� x ��� 
// �ٽ� ������ ��� x ������
// �ٽ� ������ ������ x ���� 
