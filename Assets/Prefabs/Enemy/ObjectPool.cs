using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,50)] int poolSize = 5;   // sets the poolSize for our ObjectPool
    [SerializeField] [Range(0.1f , 30f)] float spawnTimer = 1f;

    GameObject[] pool;    // here we create an array named pool

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];   // here we set pool to a new GameObject Array with size of whatever integer poolSize is.

        for (int i = 0; i < pool.Length; i++) // use pool.legth to check the amount of objects in the pool array.
        {
            pool[i] = Instantiate(enemyPrefab, transform); // here we create an enemy for each array slot we have as the loop goes on.
            pool[i].SetActive(false); // and we set it to false so it is not active in our game.
        }
    }

    void EnableObjectInPool()
    {
       for (int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false) // here we check if the object in our pool are inactive  and if they are..
            {
                pool[i].SetActive(true); // we make them active.
                return; // and escape to the if check to check again.
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
