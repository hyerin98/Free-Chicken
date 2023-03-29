using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEgg : MonoBehaviour
{
    bool isFire;
    Vector3 direction;
    [SerializeField] float speed = 1f;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        if(isFire)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerEgg>() != null)
        {
            Destroy(gameObject);
        }
    }
}
