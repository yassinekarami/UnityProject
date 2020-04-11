using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    protected Animator animator;
    protected GameObject player;
    [SerializeField] protected float distance;
    [SerializeField] protected  int health;


    // parameter for gizmo
    public float spottedDistance;
    public float attackDistance;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // subscribtion to event
        EllenController.onAttackEnemy += setDammage;
        Bullet.onHitEnemy += setDammage;

        this.animator = GetComponent<Animator>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.health = 2;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        this.distance = Vector3.Distance(transform.position, player.transform.position);
        this.animator.SetFloat("distance", this.distance);
    }

    public virtual void setDammage()
    {
        if(this.health > 0)
        {
            this.animator.SetBool("hit", true);
            this.health -= 1;
        }
    }

    public virtual void endHitAnimation()
    {
        if(this.health > 0)
        {
            this.animator.SetBool("hit", false);
        }
        if(this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void attack() { }

    public virtual void OnDrawGizmos()
    {
        // spottedDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.spottedDistance);

        // attackDistance 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, this.attackDistance);
    }
}
