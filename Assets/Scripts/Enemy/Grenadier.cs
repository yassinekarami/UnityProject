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
        hitRandom = "hitRandom";
        speed = "speed";
        health = 5;
        points = GameObject.FindGameObjectsWithTag("Point");
     
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(!stopMoving())
        {
            agent.isStopped = false;
            if (base.isTargetReached())
            {
                setTarget(points);
                animator.SetFloat(speed, agent.speed);
            }
        }
        else
        {
            agent.isStopped = true;
            Quaternion q = new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, 0f);
            transform.rotation = q;
        }
    }

    private void setTarget(GameObject[] points)
    {
        GameObject destination = points[Random.Range(0, points.Length - 1)];
        agent.SetDestination(destination.transform.position);
    }


    private bool stopMoving()
    {
        // return true si la distance est inférieur a 12 
        // return false si la distance est suppérieur
        return Vector3.Distance(transform.position, player.transform.position) < 12;
    }

    public override void setDammage()
    {
        if (health > 0)
        {
            animator.SetBool("hit", true);
            animator.SetInteger("hitRandom", Random.Range(1, 4));
            health -= 1;
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
