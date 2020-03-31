using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    GameObject staff;
    GameObject pistol;

    public float jumpForce;
    bool facingRight;
    float shootTimer;

    [SerializeField] int attack;
    [SerializeField] int shoot;
    [SerializeField] float horizontal;

    // strings for input
    string horizontalInput;
    string attackInput;
    string jumpInput;
    string shootInput;

    // strings for params
    string jumpForceParam;
    string runParam;
    string attackParam;
    string shootParam;
    string isShootingParam;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.facingRight = true;
        this.attack = 0;
        this.rb = GetComponent<Rigidbody>();
        this.staff = GameObject.FindGameObjectWithTag("Staff");
        this.staff.SetActive(false);
        this.pistol = GameObject.FindGameObjectWithTag("Pistol");
        this.pistol.SetActive(false);


        this.horizontalInput = "Horizontal";
        this.attackInput = "Attack";
        this.jumpInput = "Jump";
        this.shootInput = "Shoot";
        this.jumpForceParam = "jumpForce";
        this.runParam = "run";
        this.attackParam = "attack";
        this.shootParam = "shoot";
        this.isShootingParam = "isShooting";


    }

    // Update is called once per frame
    void Update()
    {
        this.horizontal = Input.GetAxis(this.horizontalInput);
     
        if(this.facingRight && Input.GetAxis(this.horizontalInput) < 0 || !this.facingRight && Input.GetAxis(this.horizontalInput) > 0 )
        {
            Vector3 rotate = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            rotate.z = rotate.z * -1;
            this.facingRight = !this.facingRight;
            gameObject.transform.localScale = rotate;
        }
        if(Input.GetButtonDown(this.horizontalInput))
        {
            this.attack = 0;
        }
        
        if(Input.GetButtonDown(this.attackInput))
        {
            this.attack++;
            this.staff.SetActive(true);
        }
        if(Input.GetButtonDown(this.jumpInput))
        {
            this.attack = 0;
            this.rb.AddForce(new Vector3(0, this.jumpForce, 0), ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Shoot")) // reinit timer 
        {
            this.shootTimer = 0f;
                   
            this.shoot++;
            this.pistol.SetActive(true);
        }
        this.shootTimer += Time.fixedDeltaTime;
        if (this.shootTimer > 2.0f) // fin du shoot
        {
            this.shoot = 0;
            this.pistol.SetActive(false);
        }

        this.animator.SetInteger(this.shootParam, this.shoot);
        this.animator.SetBool(this.isShootingParam, Input.GetButtonDown("Shoot"));
        this.animator.SetFloat(this.jumpForceParam, this.rb.velocity.y);
        this.animator.SetInteger(this.attackParam, this.attack);
        this.animator.SetFloat(this.runParam, Mathf.Abs(this.horizontal));
    }

    private void endAttack()
    {
        this.attack = 0;
        this.staff.SetActive(false);
    }
}
