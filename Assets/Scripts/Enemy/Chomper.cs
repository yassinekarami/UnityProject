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
        string cooldownParam = "cooldown";
        string closeObjParam = "closeObj";

        //strings for parameter

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            target = player.transform.position;
            agent.SetDestination(target);
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            animator.SetInteger(closeObjParam, closeObject.Count);
            agent.SetDestination(target);
            if (agent.remainingDistance >=1)
            {
               
                animator.SetFloat("distance", agent.remainingDistance);
            } 
            else if(agent.remainingDistance < 1)
            {
                target = player.transform.position;
                animator.SetBool(cooldownParam, true);
            }
            
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
             animator.SetBool("cooldown", false);
        }

       
    }
}
