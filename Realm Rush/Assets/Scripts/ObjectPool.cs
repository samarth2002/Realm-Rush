using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject Rig;
    [SerializeField][Range(0.1f,30f)] float SpawnTimeDelay =1f;

    [SerializeField][Range(0,50)] int PoolSize = 5;

    GameObject[] pool;
    CoordinateLabeler spawnCoord;

    void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[PoolSize];
        for(int i = 0 ; i < pool.Length; i++)
        {
            pool[i] = Instantiate(Rig , transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        spawnCoord = GetComponent<CoordinateLabeler>();
        StartCoroutine(SpawnEnemy());
    }
    void EnableInObjectPool()
    {
        for(int i = 0 ; i < pool.Length ; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }

        }
    }
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableInObjectPool();
            yield return new WaitForSeconds(SpawnTimeDelay);
        }
    }
}
