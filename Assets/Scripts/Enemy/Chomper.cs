using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ellen.UI;
namespace Enemy
{
    public class Chomper : BaseEnemy
    {
        // Chomper suit le joueur a la trace
        // il récupère sa position 
        // et quand il y arrive qui récupère sa nouvelle position

        List<GameObject> closeObject = new List<GameObject>();

        Vector3 target;

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
            distance = Vector3.Distance(transform.position, target);
            animator.SetFloat("distance", distance);

            if (target != null)
            {
                agent.SetDestination(target);

                if (base.isTargetReached())
                {
                    StartCoroutine(stopMovement());
                }
            }
        }

        public override void attack()
        {
            foreach (GameObject obj in closeObject)
            {
                if (obj.gameObject.tag == "Player")
                {
                    obj.gameObject.GetComponent<PlayerInterface>().updateHealth(-10);
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            closeObject.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            closeObject.Remove(other.gameObject);
        }
        IEnumerator stopMovement()
        {
            agent.SetDestination(gameObject.transform.position);
            yield return new WaitForSeconds(5);
            target = player.transform.position;
        }
    }
}
