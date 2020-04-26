using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Ellen.UI;
using Enemy;
using Ellen.combat;
using Ellen.move;

namespace Ellen.controller
{
    public class PlayerController : MonoBehaviour
    {
        Animator animator;
        Rigidbody rb;
   

        AudioSource audioSource;
        public NavMeshAgent agent;
        public PlayerAttackStaff playerAttackStaff;
        PlayerMove playerMove;
    
        RaycastHit hit; // raycast for shoot

        bool  isRayHit;
       
        
        float lastClickedTimeShoot;
        float shootDelay = 1.0f;
    
        [SerializeField] int hitLayer;



        // strings for params
        string speedParam = "speed";
        string isRunningParam = "isRunning";
        string attackParam = "attack";
        string shootParam = "shoot";
        string isShootingParam = "isShooting";
        string hitParam = "hit";
        string deathParam = "death";


        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            playerMove = GetComponent<PlayerMove>();
        }

        // Update is called once per frame
        void Update()
        {

            animator.SetFloat(speedParam, Mathf.Abs(agent.velocity.z));
            if (playerAttackStaff.beginAttack()) return;
            if (playerMove.move(agent)) return;

            
            //Physics.Raycast(ray, out hit, 500);
            //hits = Physics.RaycastAll(ray.origin, ray.direction, 500);
            //if (Input.GetMouseButtonDown(0) && hits != null)
            //{
            //    foreach(RaycastHit hit in hits) {
            //        hitLayer = hit.transform.gameObject.layer;

            //        Debug.Log(hit.transform.gameObject.name);
            //        switch (hitLayer)
            //        {
            //            case 9: // Enemy layer ==> 9
            //                playerAttackStaff.beginAttack();

            //                //if (Vector3.Distance(gameObject.transform.position, hit.transform.position) < 5)
            //                //{

            //                //}
            //                //else agent.SetDestination(hit.point);


            //                break;

            //            case 8: // Ground layer ==> 8
            //                agent.SetDestination(hit.point);
            //                break;

            //            default:
            //                agent.SetDestination(hit.point);
            //                break;
            //        }
            //    }
            //}

            if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<PlayerInterface>().canShoot())
            {
                GetComponent<PlayerAttackPistol>().beginShoot();
                lastClickedTimeShoot = Time.time;
                agent.isStopped = true;

            }

          //  fin du shoot
            if (Time.time - lastClickedTimeShoot > shootDelay)
            {
                GetComponent<PlayerAttackPistol>().endShoot();
                agent.isStopped = false;
            }


            animator.SetInteger(shootParam, GetComponent<PlayerAttackPistol>().getShoot());
            animator.SetBool(isShootingParam, Input.GetMouseButtonDown(1));
            animator.SetInteger(attackParam, playerAttackStaff.getAttack());


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

        public void takeDammage(int value)
        {
            if (GetComponent<PlayerInterface>().health > 0)
            {
                GetComponent<PlayerInterface>().updateHealth(value);
                animator.SetBool(hitParam, true);
            }
            if (GetComponent<PlayerInterface>().health <= 0)
            {
                animator.SetTrigger(deathParam);
            }
        }
        public void endHitAnimation()
        {
            animator.SetBool(hitParam, false);
        }

        private void instantiateBullet()
        {
            Vector3 rotation = transform.position - hit.collider.transform.position;
            transform.LookAt(hit.point);
            gameObject.GetComponent<PlayerInterface>().updateShootBar(-10);
            GetComponent<PlayerAttackPistol>().instantiateShoot(hit.point);
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

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case 10:  // Health layer ==> 9
                    GetComponent<PlayerInterface>().updateHealth(20);
                    Destroy(other.gameObject);
                    break;
                case 11: // Pistol layer ==> 9
                    GetComponent<PlayerInterface>().updateShootBar(20);
                    Destroy(other.gameObject);
                    break;
            }
        }

    }
}
