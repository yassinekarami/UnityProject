using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public GameObject explostionParticle;

    //event & delegate
    public delegate void hitEnemy();
    public static event hitEnemy onHitEnemy;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag != "Player")
    //    {
    //        Debug.Log(other.gameObject.tag);
    //        if (other.gameObject.tag == "Enemy")
    //        {
    //            onHitEnemy?.Invoke();

    //        }

    //        ContactPoint contact = other.contactOffset;
    //        explostionParticle.GetComponentInChildren<ParticleSystem>().Play();
    //        Destroy(gameObject);
    //    }

    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                onHitEnemy?.Invoke();
            }

            foreach (ContactPoint contact in collision.contacts)
            {
                GameObject particle = Instantiate(explostionParticle);
                particle.transform.position = contact.point;
                particle.GetComponentInChildren<ParticleSystem>().Play();
            }
              
            Destroy(gameObject);
        }
    }

}
