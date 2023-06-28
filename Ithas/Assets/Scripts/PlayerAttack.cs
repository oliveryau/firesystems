using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        private GameController gameController;
        private Animator animator;
        private PlayerMovement playerMovement;

        [SerializeField] private Transform attackArea;
        [SerializeField] private LayerMask enemyLayers;

        public float damage;
        public float attackRange;
        public float attackRate;
        public float nextAttackTime = 0f;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Get attack values");
            this.gameController = gameController; //set game controller reference

            animator = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>(); //for animation

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
            animator.SetFloat("Horizontal", playerMovement.movement.x); //get movement.x direction
            animator.SetFloat("Vertical", playerMovement.movement.y); //get movement.y direction
            animator.SetTrigger("Attack"); //play atk animation based on direction

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
