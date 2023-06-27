using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        private GameController gameController;

        //[SerializeField] private Animator animator;
        [SerializeField] private Transform attackArea;
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask enemyLayers;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController; //set game controller reference
        }

        private void Attack()
        {
            //animator.setTrigger("Attack"); //play atk animation

            //detect enemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayers);

            //damage
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackArea == null) return;

            Gizmos.DrawWireSphere(attackArea.position, attackRange);
        }

        #region Input Handling

        public void DoMove(Vector2 action)
        {
            //do nothing
        }

        public void DoAttack()
        {
            Debug.Log("PlayerAtk called");
            Attack();
        }

        #endregion
    }
}
