using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        // spawnpoint
        public GameObject[] enemySpawnPoints;
        public GameObject[] healthSpawnPoints;
        public GameObject[] coinSpawnPoints;
        public GameObject[] pistolSpawnPoints;

        //object numbers
        public static GameObject[] health;
        public static GameObject[] enemy;
        public static GameObject[] coin;
        public static GameObject[] pistol;

        //gameobject that will spawn
        public GameObject healthPickup;
        public GameObject[] enemies;
        public GameObject coinPickup;
        public GameObject pistolPickup;

        // ui
        public static Text healthUI;
        public static Text pistolUI;
        public static Text coinUI;


        // Start is called before the first frame update
        void Start()
        {
            healthUI = GameObject.Find("HealthPickup").GetComponent<Text>();
            pistolUI = GameObject.Find("PistolPickup").GetComponent<Text>();
            coinUI = GameObject.Find("CoinPickup").GetComponent<Text>();

            health = new GameObject[UnityEngine.Random.Range(2, 5)];
            coin = new GameObject[UnityEngine.Random.Range(2, 5)];
            pistol = new GameObject[UnityEngine.Random.Range(2, 5)];

            initGameobjectArray(health, healthSpawnPoints, healthPickup);
            initGameobjectArray(coin, coinSpawnPoints, coinPickup);
            initGameobjectArray(pistol, pistolSpawnPoints, pistolPickup);

            healthUI.text += health.Length.ToString();

            pistolUI.text += pistol.Length.ToString();

            coinUI.text += coin.Length.ToString();
        }

        private void initGameobjectArray(GameObject[] array, GameObject[] spawnPoint, GameObject obj)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = obj;
                Instantiate(obj, spawnPoint[UnityEngine.Random.Range(0, spawnPoint.Length - 1)].transform.position, obj.transform.rotation);
            }
        }

        public static void updateHealthUI()
        {
            Array.Resize(ref health, health.Length - 1);
            healthUI.text = "healthPickup : " + health.Length.ToString();
        }

        public static void updatePistolUI()
        {
            Array.Resize(ref pistol, pistol.Length - 1);
            healthUI.text = "pistolPickup : " + pistol.Length.ToString();
        }

        public static void updateCoinUI()
        {
            Array.Resize(ref coin, coin.Length - 1);
            coinUI.text = "coinPickup : " + coin.Length.ToString();
        }





        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            foreach (GameObject obj in enemySpawnPoints)
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
}

