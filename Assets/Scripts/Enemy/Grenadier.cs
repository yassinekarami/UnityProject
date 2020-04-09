using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : BaseEnemy
{
    List<GameObject> closeObject = new List<GameObject>();

    //delegate and event
    public delegate void attackPlayer();
    public static attackPlayer onAttackPlayer;

    // strings for params
    string hitRandom;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.hitRandom = "hitRandom";
        base.health = 5;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public override void setDammage()
    {
        if (this.health > 0)
        {
            base.animator.SetBool("hit", true);
            base.animator.SetInteger("hitRandom", Random.Range(1, 4));
            this.health -= 1;
        }
    }

    public override void endHitAnimation()
    {
        base.endHitAnimation();
    }
    public override void attack()
    {
       
    }
}
