using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Ellen.move
{
    public class PlayerMove : MonoBehaviour
    {
        RaycastHit hit;
        Ray ray;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public bool move(NavMeshAgent agent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 500);
                foreach (RaycastHit hit in hits)
                {
                    Debug.Log(hit.transform.gameObject);
                    // if the ray hit an enemy
                    if (hit.transform.gameObject.layer == 9 )
                    {
                        return false;
                    }
                    else if (hit.transform.gameObject.layer == 8 )
                    {
                        agent.SetDestination(hit.point);
                        return true;
                    } 
                }
                //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Physics.Raycast(ray.origin, ray.direction, out hit, 500);

                //if (hit.transform.gameObject.layer == 8)
                //{
                //    agent.SetDestination(hit.point);
                //    return true;
                //}

            }
            return false;
        }
    }
}

