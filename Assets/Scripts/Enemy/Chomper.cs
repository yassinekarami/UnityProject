using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : BaseEnemy
{

    List<GameObject> closeObject = new List<GameObject>();

    //delegate and event
    public delegate void attackPlayer();
    public static attackPlayer onAttackPlayer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void attack()
    {
        foreach(GameObject obj in closeObject)
        {
            if(obj.gameObject.tag == "Player")
            {
                onAttackPlayer?.Invoke();
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
}
