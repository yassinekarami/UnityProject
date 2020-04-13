using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenHealth : MonoBehaviour
{
    [SerializeField] int health;
    Animator animator;

    // strings for params
    string hitParam = "hit";
    string deathParam = "death";


    // Start is called before the first frame update
    void Start()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
    }


    // hit annimation
    private void beginHitAnim()
    {
        if (health > 0)
            animator.SetBool(hitParam, true);
    }
    public void takeDammage()
    {
        animator.SetBool(hitParam, false);
        health = health - 1;
        if (health <= 0)
        {
            animator.SetTrigger(deathParam);
        }
    }
}
