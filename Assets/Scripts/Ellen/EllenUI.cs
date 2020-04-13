using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EllenUI : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float shoot;

    Animator animator;
    public GameObject healthBar;
    public GameObject shootBar;

    // strings for params
    string hitParam = "hit";
    string deathParam = "death";


    // Start is called before the first frame update
    void Start()
    {
        health = healthBar.GetComponent<Image>().fillAmount * 100;
        shoot = shootBar.GetComponent<Image>().fillAmount * 100;     
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
        health = health - 10;
        healthBar.GetComponent<Image>().fillAmount = health / 100;
        if (health <= 0)
        {
            animator.SetTrigger(deathParam);
        }
    }
    public void updateShootBar()
    {
        shoot = shoot - 10;
        shootBar.GetComponent<Image>().fillAmount = shoot / 100;

    }
    public bool canShoot()
    {
        return shootBar.GetComponent<Image>().fillAmount >0 ? true:false;
    }
}
