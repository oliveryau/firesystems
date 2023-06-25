using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        private GameController gameController;
        private GameObject attackArea = default;
        private bool attacking = false;
        private float attackTime = 0.25f;
        private float timer = 0f;

        public override void Initialize(GameController aController)
        {
            gameController = aController; //set game controller reference
            attackArea = transform.gameObject; //get attack area object from first child
        }

        private void Update()
        {
            if (attacking) //from Attack()
            {
                timer += Time.deltaTime;

                if (timer >= attackTime) //stop attack after past timer
                {
                    timer = 0;
                    attacking = false;
                    //attackArea.SetActive(attacking);
                }
            }
        }

        #region Input Handling

        public void DoMove(Vector2 action)
        {
            //do nothing
        }

        public void DoAttack()
        {
            Debug.Log("PlayerAtk called");
            if (!attacking)
            {
                attacking = true;
                //attackArea.SetActive(attacking);
            }
        }

        #endregion
    }
}
