using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    GameObject staff;
    GameObject pistol;
    AudioSource audioSource;

    public GameObject pistolBulletSpawn;
    public GameObject bullet;
    public GameObject staffParticle;

    public float jumpForce;
    float shootTimer;
    bool isGrounded;

    [SerializeField] int attack;
    [SerializeField] int shoot;
    [SerializeField] float vertical;
    [SerializeField] float horizontal;


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
    
    

    // delegate and event
    public delegate void attackEnemy();
    public static event attackEnemy onAttackEnemy;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        attack = 0;
        rb = GetComponent<Rigidbody>();
        staff = GameObject.FindGameObjectWithTag("Staff");
        staff.SetActive(false);
        pistol = GameObject.FindGameObjectWithTag("Pistol");
        pistol.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis(verticalInput);
        horizontal = Input.GetAxis(horizontalInput);

        rotate();

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

        // reinit timer 
        if (Input.GetButtonDown("Shoot") && gameObject.GetComponent<EllenUI>().canShoot())
        {
            shootTimer = 0f;       
            shoot++;
            pistol.SetActive(true);
        }

        // fin du shoot
        shootTimer += Time.fixedDeltaTime;
        if (shootTimer > 1.0f) 
        {
            shoot = 0;
            pistol.SetActive(false);
        }

        animator.SetInteger(shootParam, shoot);
        animator.SetBool(isShootingParam, Input.GetButtonDown("Shoot"));
        animator.SetFloat(jumpForceParam, rb.velocity.y);
        animator.SetInteger(attackParam, attack);


        if (Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0)
        {
           
            animator.SetBool(isRunningParam, true);
            animator.SetFloat(runVerticalParam, vertical);
            animator.SetFloat(runHorizontalParam, horizontal);
        }
        else
        {
            audioSource.Stop();
            animator.SetBool(isRunningParam, false);
        }
    }

  
    private void startFootstep()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
   
    private void endAttack()
    {
        attack = 0;
        staff.SetActive(false);
    }
         
    private void instantiateBullet()
    {
        gameObject.GetComponent<EllenUI>().updateShootBar();
        GameObject instantiateBullet =  Instantiate(bullet, pistolBulletSpawn.transform.position, Quaternion.identity);
        instantiateBullet.transform.rotation = transform.rotation;
    }

    private void rotate()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse Y"), 0) * 5);
    }


    // collision & trigger funtion

    public void OnTriggerStay(Collider other)
    {
       
        if (attack > 0 && other.gameObject.tag == "Enemy")
        {
            if (Input.GetButtonDown(attackInput))
            {
                other.gameObject.GetComponent<BaseEnemy>().setDammage();
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
