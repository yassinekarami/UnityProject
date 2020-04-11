using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Grenadier : BaseEnemy
{
    List<GameObject> closeObject = new List<GameObject>();

    public GameObject grenade;
    public GameObject grenadeSpawn;
    public GameObject[] points;

    NavMeshAgent agent;

    //delegate and event
    public delegate void attackPlayer();
    public static attackPlayer onAttackPlayer;

    // strings for params
    string hitRandom;
    string speed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.hitRandom = "hitRandom";
        this.speed = "speed";
        base.health = 5;
        points = GameObject.FindGameObjectsWithTag("Point");
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(!stopMoving())
        {
            this.agent.isStopped = false;
            if (isTargetReached())
            {
                setTarget(points);
                base.animator.SetFloat(this.speed, agent.speed);
            }
        }
        else
        {
            this.agent.isStopped = true;
        }
    }

    private void setTarget(GameObject[] points)
    {
        GameObject destination = points[Random.Range(0, points.Length - 1)];
        agent.SetDestination(destination.transform.position);
    }

    private bool isTargetReached()
    {
        bool res = false;
        if(agent.remainingDistance <=1)
        {
            res = true ;
        }
        return res;
    }

    private bool stopMoving()
    {
        // return true si la distance est inférieur a 12 
        // return false si la distance est suppérieur
        return Vector3.Distance(transform.position, base.player.transform.position) < 12;
    }

    public override void setDammage()
    {
        if (this.health > 0)
        {
            base.animator.SetBool("hit", true);
            base.animator.SetInteger("hitRandom", Random.Range(1, 4));
            this.health -= 1;
        }
    }

    public override void endHitAnimation()
    {
        base.endHitAnimation();
    }
    public override void attack()
    {
        Instantiate(grenade, grenadeSpawn.transform.position, Quaternion.identity);
    }
}
