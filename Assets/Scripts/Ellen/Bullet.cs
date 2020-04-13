using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    float timer;

    public GameObject explostionParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        timer += Time.fixedDeltaTime;
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
            }
        }
    }

    IEnumerator destroyParticle(GameObject p)
    {
        float duration = p.GetComponent<ParticleSystem>().main.duration;
     
        yield return new WaitForSeconds(duration + 1);
        Destroy(p);
        Destroy(gameObject);
    }


}
