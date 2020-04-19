using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ellen.attack
{
    public class PlayerAttackStaff : MonoBehaviour
    {
        GameObject staff;

        [SerializeField] int nbattack;
        // Start is called before the first frame update
        void Start()
        {
            staff = GameObject.FindGameObjectWithTag("Staff");
            staff.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void beginAttack()
        {
            nbattack  ++;
            staff.SetActive(true);
        }

        public void endAttack()
        {
            nbattack = 0;
            staff.SetActive(false);
        }

        public void setAttack(int value)
        {
            nbattack = value;
        }

        public int getAttack()
        {
            return nbattack;
        }
    }
}
