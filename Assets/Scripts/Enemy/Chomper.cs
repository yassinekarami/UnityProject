using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ellen.controller;
namespace Enemy
{
    public class Chomper : BaseEnemy
    {
        // Chomper suit le joueur a la trace
        // il récupère sa position 
        // et quand il y arrive qui récupère sa nouvelle position

        List<GameObject> closeObject = new List<GameObject>();

        Vector3 target;

        //string params
        string closestObjParam = "closestObj";
        string cooldownParam = "cooldown";
        string closeObjParam = "closeObj";

        //strings for parameter

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            target = player.transform.position;
        }

        // Update is called once per frame
        public override void Update()
        {
            animator.SetFloat(distanceParam, agent.remainingDistance);
            agent.SetDestination(target);
 
            animator.SetInteger(closeObjParam, closeObject.Count);
            if (base.isTargetReached())
            {
                animator.SetBool(cooldownParam, true);
            }
           
            //if (target != null)
            //{
            //    agent.SetDestination(target);
            //    distance = Vector3.Distance(transform.position, target);
            //    animator.SetFloat("distance", distance);
            //    animator.SetInteger(closestObjParam, closeObject.Count);
           

            //    if (base.isTargetReached())
            //    {
            //        StartCoroutine(stopMovement());
            //    }
            //}
        }

        public override void attack()
        {
            if(closeObject != null)
            {
                foreach (GameObject obj in closeObject)
                {
                    obj.GetComponent<PlayerController>().takeDammage(-10);
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                closeObject.Add(other.gameObject);
            }
               
        }

        private void OnTriggerExit(Collider other)
        {
            closeObject.Remove(other.gameObject);
        }

        public void endCooldown()
        {
            target = player.transform.position;
            animator.SetBool(cooldownParam, false);
            animator.SetFloat("distance", agent.remainingDistance);
            transform.LookAt(target);
            agent.SetDestination(target);
           
        }
    }
}
