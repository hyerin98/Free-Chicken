using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackSpace : MonoBehaviour
{
    Boss boss;
    public GameObject prefab;
    public GameObject dropRock;
    BoxCollider area;
    public int cnt = 3;
    GameObject go;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        area = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isRandomSpace)
        {
            for (int i = 0; i < cnt; i++)
            {
                Spawn();

            }
        }
        boss.isRandomSpace = false;
    }
    

    void Spawn()
    {
        pos = GetRandomPos();
        
        GameObject go = prefab;
        GameObject instance = Instantiate(go, pos, Quaternion.identity);

        StartCoroutine(SpawnRock(pos));
        Destroy(instance, 3f);
        
    }
    IEnumerator SpawnRock(Vector3 pos)
    {
        yield return new WaitForSeconds(3f);
        Vector3 newPos = pos + new Vector3(0f,10f,0f);
        for (int i = 0; i < cnt; i++)
        {
            GameObject rock = dropRock;
            GameObject ins = Instantiate(rock, newPos, Quaternion.identity);
            Destroy(ins, 3f);
           
        }
       

    }
    Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, -1.2f, posZ);
        Vector3 pos = area.center + spawnPos;
        return pos;
    }
}
