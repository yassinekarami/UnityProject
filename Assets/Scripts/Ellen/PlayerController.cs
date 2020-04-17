using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Ellen.UI;
using Enemy;
using Weapon.player;
namespace Ellen.controller
{
    public class PlayerController : MonoBehaviour
    {
        Animator animator;
        Rigidbody rb;
        GameObject staff;
        GameObject pistol;
        AudioSource audioSource;
        NavMeshAgent agent;
        RaycastHit hit;
        Ray ray;


        public GameObject pistolBulletSpawn;
        public GameObject bullet;
        public GameObject staffParticle;

        public float jumpForce;
        float shootTimer;
        bool isRayHit;
        float lastClickedTime;
        float attackDelay = 0.9f;

        [SerializeField] int attack;
        [SerializeField] int shoot;
        [SerializeField] int hitLayer;



        // strings for input

        string attackInput = "Attack";
        string shootInput = "Shoot";

        // strings for params
        string jumpForceParam = "jumpForce";
        string speedParam = "speed";
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
            agent = GetComponent<NavMeshAgent>();
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
            animator.SetFloat(speedParam, Mathf.Abs(agent.velocity.z));

            if (Time.time - lastClickedTime > attackDelay)
            {
                attack = 0;
            }


            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isRayHit = Physics.Raycast(ray, out hit, 500);

            if (Input.GetMouseButtonDown(0) && isRayHit)
            {
                hitLayer = hit.transform.gameObject.layer;
                if (hitLayer != 9)
                {
                    agent.SetDestination(hit.point);
                }
                //switch(hitLayer)
                //{
                //    case 9: // Enemy layer ==> 9
                //        attack++;
                //        lastClickedTime = Time.time;
                //        staff.SetActive(true);
                //        break;

                //    case 8: // Ground layer ==> 8
                //        agent.SetDestination(hit.point);
                //        break;

                //    default:
                //        Debug.Log("Other");
                //        break;
                //}
            }

            // reinit timer 
            if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<PlayerInterface>().canShoot())
            {
                shootTimer = 0f;
                shoot++;
                pistol.SetActive(true);
                agent.SetDestination(transform.position);
            }

            // fin du shoot
            shootTimer += Time.fixedDeltaTime;
            if (shootTimer > 1.0f)
            {
                shoot = 0;
                pistol.SetActive(false);
            }

            animator.SetInteger(shootParam, shoot);
            animator.SetBool(isShootingParam, Input.GetMouseButtonDown(1));
            animator.SetFloat(jumpForceParam, rb.velocity.y);
            animator.SetInteger(attackParam, attack);


            if (Mathf.Abs(agent.velocity.z) > 0)
            {
                animator.SetBool(isRunningParam, true);
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
            Vector3 rotation = transform.position - hit.collider.transform.position;
            transform.LookAt(hit.point);
            gameObject.GetComponent<PlayerInterface>().updateShootBar(-10);
            GameObject instantiateBullet = Instantiate(bullet, pistolBulletSpawn.transform.position, Quaternion.identity);
            Vector3 target = new Vector3(hit.point.x, pistolBulletSpawn.transform.position.y, hit.point.z);
            instantiateBullet.GetComponent<Bullet>().setTarget(target);

        }

        private void moveCharacter()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 500))
                {
                    agent.SetDestination(hit.point);
                }
            }
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

    }
}
