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

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray.origin, ray.direction, out hit, 500);

                if (hit.transform.gameObject.layer == 8)
                {
                    agent.SetDestination(hit.point);
                    return true;
                }
            }
            return false;
        }
    }
}

