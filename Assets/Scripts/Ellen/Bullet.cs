using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // mettre un event a la place
            other.gameObject.BroadcastMessage("getDammage");
            Destroy(this.gameObject);
        }
    }
}
