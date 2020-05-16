using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Grenadier : BaseEnemy
    {
        List<GameObject> closeObject = new List<GameObject>();

        public GameObject grenade;
        public GameObject grenadeSpawn;
        public GameObject[] points;

        // strings for params
        string hitRandom = "hitRandom";
        string speed = "speed";

        // Start is called before the first frame update
        public override void Start()
        {
            points = GameObject.FindGameObjectsWithTag("Point");
            health = 5;
            base.Start();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            distance = Vector3.Distance(transform.position, player.transform.position);
            animator.SetFloat(distanceParam, distance);
            if (!stopMoving())
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
            GameObject destination = points[Random.Range(0, points.Length)];
            agent.SetDestination(destination.transform.position);
        }


        private bool stopMoving()
        {
            // return true si la distance est inférieur a 12 
            // return false si la distance est suppérieur
            transform.LookAt(player.transform.position);
            return Vector3.Distance(transform.position, player.transform.position) < 12;
        }

        public override void setDammage()
        {
            base.setDammage();
            if(health > 0)
                animator.SetInteger(hitRandom, Random.Range(1, 4));
        }

        public override void endHitAnimation()
        {
            base.endHitAnimation();
        }
        public override void attack()
        {
            //  transform.rotation = Quaternion.LookRotation(player.transform.position);
            Instantiate(grenade, grenadeSpawn.transform.position, Quaternion.identity);    
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach(GameObject point in points)
            {
                Gizmos.DrawSphere(point.transform.position, 1f);
            }
        }
    }
}
