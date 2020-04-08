using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    Animator animator;
    GameObject player;
    [SerializeField] float distance;

    // parameter for gizmo
    public float spottedDistance;
    public float attackDistance;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.distance = Vector3.Distance(transform.position, player.transform.position);
        this.animator.SetFloat("distance", this.distance);
    }

    public void getDammage()
    {
        this.animator.SetBool("hit", true);
    }

    public void endHitAnimation()
    {
        this.animator.SetBool("hit", false);
    }

    private void OnDrawGizmos()
    {
        // spottedDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.spottedDistance);

        // attackDistance 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, this.attackDistance);
    }
}
