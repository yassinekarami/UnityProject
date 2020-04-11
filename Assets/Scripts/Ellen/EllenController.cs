using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    GameObject staff;
    GameObject pistol;
    public GameObject bullet;
    public GameObject staffParticle;

    public float jumpForce;
    bool facingRight;
    float shootTimer;
    bool isGrounded;

    [SerializeField] int attack;
    [SerializeField] int shoot;
    [SerializeField] float vertical;
    [SerializeField] float horizontal;
    [SerializeField] int health;

    // strings for input
    string horizontalInput = "Horizontal";
    string verticalInput = "Vertical";
    string attackInput = "Attack";
    string jumpInput = "Jump";
    string shootInput = "Shoot";

    // strings for params
    string jumpForceParam = "jumpForce";
    string runHorizontalParam = "runHorizontal";
    string runVerticalParam = "runVertical";
    string isRunningParam = "isRunning";
    string attackParam = "attack";
    string shootParam = "shoot";
    string isShootingParam = "isShooting";
    string deathParam = "death";
    string hitParam = "hit";
    string turnParam  = "turn";

    // delegate and event
    public delegate void attackEnemy();
    public static event attackEnemy onAttackEnemy;


    // Start is called before the first frame update
    void Start()
    {
        Chomper.onAttackPlayer += beginHitAnim;
        Grenade.onAttackPlayer += beginHitAnim;

        animator = GetComponent<Animator>();
        facingRight = true;
        attack = 0;
        rb = GetComponent<Rigidbody>();
        staff = GameObject.FindGameObjectWithTag("Staff");
        staff.SetActive(false);
        pistol = GameObject.FindGameObjectWithTag("Pistol");
        pistol.SetActive(false);
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis(verticalInput);
        horizontal = Input.GetAxis(horizontalInput);

        rotate();

        //if (facingRight && Input.GetAxis(verticalInput) < 0 || !facingRight && Input.GetAxis(verticalInput) > 0)
        //{
        //    Vector3 rotate = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        //    rotate.z = rotate.z * -1;
        //    facingRight = !facingRight;
        //    gameObject.transform.localScale = rotate;
        //}
        if (Input.GetButtonDown(verticalInput))
        {
            attack = 0;
        }
        
        if(Input.GetButtonDown(attackInput))
        {
            attack++;
            staff.SetActive(true);
        }
        if(Input.GetButtonDown(jumpInput) && isGrounded )
        {
            attack = 0;
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Shoot")) // reinit timer 
        {
            shootTimer = 0f;       
            shoot++;
            pistol.SetActive(true);
        }
        shootTimer += Time.fixedDeltaTime;
        if (shootTimer > 2.0f) // fin du shoot
        {
            shoot = 0;
            pistol.SetActive(false);
        }

        animator.SetInteger(shootParam, shoot);
        animator.SetBool(isShootingParam, Input.GetButtonDown("Shoot"));
        animator.SetFloat(jumpForceParam, rb.velocity.y);
        animator.SetInteger(attackParam, attack);
       
        
        if(Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0)
        {
            animator.SetBool(isRunningParam, true);
            animator.SetFloat(runVerticalParam, vertical);
            animator.SetFloat(runHorizontalParam, horizontal);
        }
        else animator.SetBool(isRunningParam, false);

    }

    private void endAttack()
    {
        attack = 0;
        staff.SetActive(false);
    }

    // hit annimation
    private void beginHitAnim()
    {
        if(health > 0)
            animator.SetBool(hitParam, true);
    }
    private void takeDammage()
    {
        animator.SetBool(hitParam, false);
        health = health - 1;
        if(health <=0 )
        {
            animator.SetTrigger(deathParam);
            Chomper.onAttackPlayer -= takeDammage;
        }
    }
         
    private void instantiateBullet()
    {
     //   GameObject instantiateBullet =  Instantiate(bullet, pistol.transform.position, Quaternion.identity);
     //   instantiateBullet.transform.rotation = transform.rotation;
    }

    private void rotate()
    {
      //  Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      //  transform.Rotate(new Vector3(0, Input.GetAxis("Mouse Y"), 0) * 20);
    }


    // collision & trigger funtion

    public void OnTriggerStay(Collider other)
    {
        if(attack > 0 && other.gameObject.tag == "Enemy")
        {
            if (Input.GetButtonDown(attackInput))
            {
                onAttackEnemy?.Invoke();
                staffParticle.GetComponentInChildren<ParticleSystem>().Play();
            }
        } 
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8 && !isGrounded)
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 8 && isGrounded)
        {
            isGrounded = false;
        }
    }
}
