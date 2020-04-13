using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected GameObject player;
    [SerializeField] protected float distance;
    [SerializeField] protected  int health;
    protected NavMeshAgent agent;


    //string params
    string distanceParam = "distance";

    // parameter for gizmo
    public float spottedDistance;
    public float attackDistance;

    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        health = 2;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        animator.SetFloat(distanceParam, distance);
    }

    public virtual void setDammage()
    {
        if(health > 0)
        {
            animator.SetBool("hit", true);
            health -= 1;
        }
    }

    public virtual void endHitAnimation()
    {
        if(health > 0)
        {
            animator.SetBool("hit", false);
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void attack() { }


    public virtual bool isTargetReached()
    {
        bool res = false;
        if (agent.remainingDistance <= 1)
        {
            res = true;
        }
        return res;
    }



    public virtual void OnDrawGizmos()
    {
        // spottedDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spottedDistance);

        // attackDistance 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
