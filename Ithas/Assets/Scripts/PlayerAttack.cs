using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        private GameObject attackArea = default;
        private bool attacking = false;
        private float attackTime = 0.25f;
        private float timer = 0f;

        private void Start()
        {
            attackArea = transform.GetChild(0).gameObject; //get attack area object from first child
        }

        private void Update()
        {
            if (attacking) //from Attack()
            {
                timer += Time.deltaTime;
                Debug.Log("hi");

                if (timer >= attackTime) //stop attack after past timer
                {
                    timer = 0;
                    attacking = false;
                    attackArea.SetActive(attacking);
                    Debug.Log("bye");
                }
            }
        }

        public void DoMove(Vector2 action)
        {
            //do nothing
        }

        public void DoAttack()
        {
            if (!attacking)
            {
                attacking = true;
                attackArea.SetActive(attacking);
            }
        }
    }
}
