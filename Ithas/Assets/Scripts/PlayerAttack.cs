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
        public float attackRate;
        public float nextAttackTime = 0f;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Attack Initialized");
            this.gameController = gameController; //set game controller reference

            animator = GetComponent<Animator>();

            damage = gameController.GetPlayerDamage();
            attackRange = gameController.GetPlayerAttackRange();
            attackRate = gameController.GetPlayerAttackRate();
        }

        public void UpdatePlayerAttackStats() //everytime when level up
        {
            damage = gameController.GetPlayerDamage();
            attackRange = gameController.GetPlayerAttackRange();
            attackRate = gameController.GetPlayerAttackRate();
        }

        private void Attack()
        {
            animator.SetTrigger("Attack"); //play atk animation

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackArea.position, attackRange, enemyLayers); //detect enemies in range

            foreach (Collider2D enemy in hitEnemies) //damage code
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
            nextAttackTime = Time.time + 1f / attackRate; //higher attackRate = faster attack speed
        }

        #endregion
    }
}
