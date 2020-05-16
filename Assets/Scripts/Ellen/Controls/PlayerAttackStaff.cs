using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enemy;
using Ellen.move;

namespace Ellen.combat
{
    public class PlayerAttackStaff : MonoBehaviour
    {
    
        [SerializeField] int nbattack;
        public float attackRange;
        public GameObject staffParticle;

        NavMeshAgent agent;

        RaycastHit[] hits;
        Ray ray;

        float attackDelay = 0.9f;
        float lastClickedTime;


        // strings for input
        string attackInput = "Attack";
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
          
            agent = GetComponentInParent<NavMeshAgent>();
            //attackRange = agent.stoppingDistance;
        }

        // Update is called once per frame
        void Update()
        {
            // combot time
            if (Time.time - lastClickedTime > attackDelay)
            {
                endAttack();
            }
        }
        public bool isInRange(GameObject enemy)
        {
            Debug.Log(Vector3.Distance(transform.position, enemy.transform.position));
            if (Vector3.Distance(transform.position, enemy.transform.position) < attackRange) return true;
            return false;
        }
        public bool beginAttack()
        {
         
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray.origin, ray.direction, 500);
            if(Input.GetMouseButtonDown(0))
            {
                foreach (RaycastHit hit in hits)
                {
                    // si ce qu'on a toucher n'est pas un enemy
                    if (hit.transform.gameObject.GetComponent<BaseEnemy>() == null) continue;

                    else 
                    {
                        if (Vector3.Distance(transform.position, hit.transform.position) > attackRange)
                        {
                            agent.SetDestination(hit.point);
                            return false;
                        }
                        
                        nbattack++;
                        lastClickedTime = Time.time;
                        gameObject.SetActive(true);
                        return true;
                    }
                } 
            }
            return false;
        }

        public void endAttack()
        {
            nbattack = 0;
            gameObject.SetActive(false);
        }

        public void setAttack(int value)
        {
            nbattack = value;
        }

        public int getAttack()
        {
            return nbattack;
        }

        public void OnTriggerStay(Collider other)
        {
            BaseEnemy baseEnemy = other.gameObject.GetComponent<BaseEnemy>();
            if (baseEnemy != null && getAttack() > 0)
            {
                if (Input.GetButtonDown(attackInput))
                {
                    baseEnemy.setDammage();
                    staffParticle.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
            else return;
        }
    }
}
