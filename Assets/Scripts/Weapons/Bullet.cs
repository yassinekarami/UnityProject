using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace Weapon.player
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        float timer;
        Vector3 target;

        public GameObject explostionParticle;

        // Start is called before the first frame update
        void Start()
        {

            transform.LookAt(getTarget());
        }

        // Update is called once per frame
        void Update()
        {

            //  transform.position += transform.forward * Time.deltaTime * speed;
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            if (timer > 5f) Destroy(gameObject);
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag != "Player")
        //    {
        //        Debug.Log(other.gameObject.tag);
        //        if (other.gameObject.tag == "Enemy")
        //        {
        //            onHitEnemy?.Invoke();
        //            Debug.Log("iciiiii");
        //        }

        //    //    ContactPoint contact = other.contactOffset;
        //    //    explostionParticle.GetComponentInChildren<ParticleSystem>().Play();
        //        Destroy(gameObject);
        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                if (collision.gameObject.tag == "Enemy")
                {

                    collision.gameObject.GetComponent<BaseEnemy>().setDammage();
                }

                foreach (ContactPoint contact in collision.contacts)
                {
                    GameObject particle = Instantiate(explostionParticle);
                    particle.transform.position = contact.point;
                    particle.GetComponentInChildren<ParticleSystem>().Play();
                    StartCoroutine(destroyParticle(particle));
                    Destroy(gameObject);
                }
            }
        }

        public void setTarget(Vector3 destination)
        {
            target = destination;
        }

        private Vector3 getTarget()
        {
            return target;
        }

        IEnumerator destroyParticle(GameObject p)
        {
            float duration = p.GetComponent<ParticleSystem>().main.duration;

            yield return new WaitForSeconds(duration + 1);
            Destroy(p);
          
        }

    }
}
