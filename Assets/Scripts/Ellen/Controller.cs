using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Animator animator;
    [SerializeField] float horizontal;
    bool facingRight;
    [SerializeField] int attack;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.facingRight = true;
        this.attack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.horizontal = Input.GetAxis("Horizontal");
     
      
        if(this.facingRight && Input.GetAxis("Horizontal") < 0 || !this.facingRight && Input.GetAxis("Horizontal") > 0 )
        {
            Vector3 rotate = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            rotate.z = rotate.z * -1;
            this.facingRight = !this.facingRight;
            gameObject.transform.localScale = rotate;
        }
        if(Input.GetButtonDown("Horizontal"))
        {
            this.attack = 0;
        }
        
        if(Input.GetButtonDown("Attack"))
        {
            this.attack++;
        }
        this.animator.SetInteger("attack", this.attack);
        this.animator.SetFloat("run", Mathf.Abs(this.horizontal));
    }

  

    private void endAttack()
    {
        this.attack = 0;
    }
}
