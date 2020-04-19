using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon.player;

namespace Ellen.attack
{
    public class PlayerAttackPistol : MonoBehaviour
    {
        GameObject pistol;
        public GameObject pistolBulletSpawn;
        public GameObject bullet;


        [SerializeField] int shoot;

        // Start is called before the first frame update
        void Start()
        {
            pistol = GameObject.FindGameObjectWithTag("Pistol");
            pistol.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void  beginShoot()
        {
            pistol.SetActive(true);
            shoot++;
        }

        public void endShoot()
        {
            shoot = 0;
            pistol.SetActive(false);
        }
        
        public int getShoot()
        {
            return shoot;
        }

        public void instantiateShoot(Vector3 hitPoint)
        {
            GameObject instantiateBullet = Instantiate(bullet, pistolBulletSpawn.transform.position, Quaternion.identity);
            Vector3 target = new Vector3(hitPoint.x, pistolBulletSpawn.transform.position.y, hitPoint.z);
            instantiateBullet.GetComponent<Bullet>().setTarget(target);
        }
    }

}