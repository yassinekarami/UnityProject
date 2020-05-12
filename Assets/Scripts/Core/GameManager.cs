using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // spawnpoint
    public GameObject[] enemySpawnPoints;
    public GameObject[] healthSpawnPoints;
    public GameObject[] coinSpawnPoints;
    public GameObject[] pistolSpawnPoints;

    //object numbers
    public GameObject[] health;
    public GameObject[] enemy;
    public GameObject[] coin;
    public GameObject[] pistol;

    //gameobject that will spawn
    public GameObject healthPickup;
    public GameObject[] enemies;
    public GameObject coinPickup;
    public GameObject pistolPickup;

    // Start is called before the first frame update
    void Start()
    {
        health = new GameObject[Random.Range(2,5)];
        coin = new GameObject[Random.Range(2, 5)];
        pistol = new GameObject[Random.Range(2, 5)];

        initGameobjectArray(health, healthSpawnPoints, healthPickup);
        initGameobjectArray(coin, coinSpawnPoints, coinPickup);
        initGameobjectArray(pistol, pistolSpawnPoints, pistolPickup);
    
    }

    private void initGameobjectArray(GameObject[] array, GameObject[] spawnPoint, GameObject obj)
    {
        for(int i=0;i<array.Length;i++)
        {
            array[i] = obj;
            Instantiate(obj, spawnPoint[Random.Range(0, spawnPoint.Length - 1)].transform.position, obj.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        foreach(GameObject obj in enemySpawnPoints)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }

        Gizmos.color = Color.red;
        foreach (GameObject obj in healthSpawnPoints)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }

        Gizmos.color = Color.yellow;
        foreach (GameObject obj in coinSpawnPoints)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }

        Gizmos.color = Color.blue;
        foreach (GameObject obj in pistolSpawnPoints)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }

    }
}
