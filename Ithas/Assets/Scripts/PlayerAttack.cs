using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        private GameController gameController;
        private Animator animator;

        [SerializeField] private Transform attackArea;
        [SerializeField] private LayerMask enemyLayers;

        public float damage;
        public float attackRange;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Attack Initialized");
            this.gameController = gameController; //set game controller reference

            animator = GetComponent<Animator>();

            damage = gameController.GetPlayerDamage();
            attackRange = gameController.GetPlayerAttackRange();

            //base.Initialize(gameController);
        }

        private void Attack()
        {
            animator.SetTrigger("Attack"); //play atk animation

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
            Attack();
        }

        #endregion
    }
}
