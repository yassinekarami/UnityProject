using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float horizontal;
    [SerializeField] bool grounded;
    public float speed;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.horizontal = Input.GetAxis("Horizontal");
        this.rb.velocity = new Vector3(this.horizontal, 0, 0) * this.speed * Time.deltaTime;

        if(Input.GetButtonDown("Jump"))
        {
            if(this.grounded) // saute
            {
                this.rb.AddForce(new Vector3(0, this.jumpForce, 0), ForceMode.Force);
                this.grounded = false;
            }
            else // saute pas 
            {

            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            this.grounded = true;
        }
    }


}
