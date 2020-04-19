using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ellen.controller;
namespace Weapon.enemy
{
    public class Grenade : MonoBehaviour
    {
        GameObject player;
        Vector3 target;
        public float explosionRadius;
        // particle system to be emitted
        public GameObject fireCirle;

        private Rigidbody rb;
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        public delegate void attackPlayer();
        public static attackPlayer onAttackPlayer;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform.position;
            rb = GetComponent<Rigidbody>();
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            launchProjectile();
        }

        private void launchProjectile()
        {
            Vector3 vo = calculateVelocity(target, transform.position, 0.3f);
            rb.velocity = vo;
        }


        private Vector3 calculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            // define the distance x and y first
            Vector3 distance = target - origin;
            Vector3 distanceXZ = distance;
            distanceXZ.y = 0f;


            //create a float the represent our distance
            float sy = distance.y;
            float sxz = distance.magnitude;

            float vxz = sxz / time;
            float vy = sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            Vector3 result = distanceXZ.normalized;
            result *= vxz;
            result.y = vy;


            return result;
        }


        private void OnTriggerEnter(Collider other)
        {
        
            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, explosionRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag == "Player")
                {
                    //PlayerController player = hitColliders[i].gameObject.GetComponent<PlayerController>();
                    //player.takeDammage(-10);
                    hitColliders[i].gameObject.GetComponent<PlayerController>().takeDammage(-10);
                    Destroy(gameObject);
                }
               
                //GameObject fire = Instantiate(fireCirle, player.gameObject.transform.position, Quaternion.identity);
                //fire.GetComponent<ParticleSystem>().Play();
                //StartCoroutine(destroyParticle(fire));
            }
        }



        IEnumerator destroyParticle(GameObject f)
        {
            float duration = f.GetComponent<ParticleSystem>().main.duration;
            yield return new WaitForSeconds(duration + 1);
            Destroy(f);
            yield return new WaitForSeconds(duration + 3);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(gameObject.transform.position, explosionRadius);
        }
    }
}

