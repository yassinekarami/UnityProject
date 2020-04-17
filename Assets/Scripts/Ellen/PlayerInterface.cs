using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ellen.UI
{
    public class PlayerInterface : MonoBehaviour
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
        public void updateHealth(float value)
        {
            animator.SetBool(hitParam, false);
            health = health + value;

            healthBar.GetComponent<Image>().fillAmount = health / 100;
            Mathf.Clamp(healthBar.GetComponent<Image>().fillAmount, 0, 100);
            if (health <= 0)
            {
                animator.SetTrigger(deathParam);
            }


        }
        public void updateShootBar(float value)
        {
            shoot = shoot + value;
            shootBar.GetComponent<Image>().fillAmount = shoot / 100;
            Mathf.Clamp(shootBar.GetComponent<Image>().fillAmount, 0, 100);

        }
        public bool canShoot()
        {
            return shootBar.GetComponent<Image>().fillAmount > 0 ? true : false;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case 10:  // Health layer ==> 9
                    updateHealth(20);
                    Destroy(other.gameObject);
                    break;
                case 11: // Pistol layer ==> 9
                    updateShootBar(20);
                    Destroy(other.gameObject);
                    break;

            }
        }
    }

}