﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Ellen.controller;
namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]

    public class BaseEnemy : MonoBehaviour
    {
        protected Animator animator;
        protected GameObject player;
        [SerializeField] protected float distance;
        [SerializeField] protected int health;
        public NavMeshAgent agent;
        
        protected Slider healthSlider;

      
        //string params
        protected string distanceParam = "distance";
        protected string playerDeathParam = "playerDeath";

        // parameter for gizmo
        public float spottedDistance;
        public float attackDistance;

        // Start is called before the first frame update
        public virtual void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");

            healthSlider = GetComponentInChildren<Slider>();
            healthSlider.maxValue = health;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            if (player.GetComponent<PlayerController>().isDead)
            {
                animator.SetBool(playerDeathParam, player.GetComponent<PlayerController>().isDead);
                agent.isStopped = true;
                return;
            }
        }

        public virtual void setDammage()
        {
            if (health > 0)
            {
                animator.SetBool("hit", true);
                health -= 1;
                healthSlider.value = health;
            }
        }

        public virtual void endHitAnimation()
        {
            if (health > 0)
            {
                animator.SetBool("hit", false);
            }
            if (health <= 0)
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

        public virtual void OnDrawGizmosSelected()
        {
            // spottedDistance
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spottedDistance);

            // attackDistance 
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

}

