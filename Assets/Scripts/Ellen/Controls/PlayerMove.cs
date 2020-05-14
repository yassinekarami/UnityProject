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

        public bool move(NavMeshAgent agent)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 500);
                foreach (RaycastHit hit in hits)
                {
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
            }
            return false;
        }
    }
}

