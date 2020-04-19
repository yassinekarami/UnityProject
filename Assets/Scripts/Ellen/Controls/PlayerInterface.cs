using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ellen.UI
{
    public class PlayerInterface : MonoBehaviour
    {
        public float health;
        public float shoot;

        public GameObject healthBar;
        public GameObject shootBar;
       

        // Start is called before the first frame update
        void Start()
        {
            health = healthBar.GetComponent<Image>().fillAmount * 100;
            shoot = shootBar.GetComponent<Image>().fillAmount * 100;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void updateHealth(float value)
        {
            health = health + value;
            healthBar.GetComponent<Image>().fillAmount = health / 100;
            Mathf.Clamp(healthBar.GetComponent<Image>().fillAmount, 0, 100);
           
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
    }

}