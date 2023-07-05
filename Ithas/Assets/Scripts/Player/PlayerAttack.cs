using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ithas
{
    public class PlayerAttack : PlayerScript, InputReceiver
    {
        [Header("Stats")]
        public float damage;
        public float attackRange;
        public float attackRate;
        public float nextAttackTime = 0f; //tracker

        [Header("Others")]
        [SerializeField] private LayerMask enemyLayers;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        private GameController gameController;
        private PlayerMovement playerMovement;
        private PlayerUi playerUi;
        private Animator animator;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController; //set game controller reference
            playerMovement = GetComponent<PlayerMovement>(); //for animation

            //set SO stats first
            playerStatsSO.damage = gameController.GetPlayerDamage();
            playerStatsSO.attackRange = gameController.GetPlayerAttackRange();
            playerStatsSO.attackRate = gameController.GetPlayerAttackRate();

            //set to initial SO stats
            playerStatsSO.initialDamage = playerStatsSO.damage;
            playerStatsSO.initialAttackRange = playerStatsSO.attackRange;
            playerStatsSO.initialAttackRate = playerStatsSO.attackRate;

            //set public variables based on SO
            damage = playerStatsSO.damage;
            attackRange = playerStatsSO.attackRange;
            attackRate = playerStatsSO.attackRate;
        }

        public void UpdatePlayerAttackStats() //level up, update new stats
        {
            gameController = FindObjectOfType<GameController>();
            playerUi = GetComponent<PlayerUi>();

            //set SO stats first
            playerStatsSO.damage = gameController.GetPlayerDamage();
            playerStatsSO.attackRange = gameController.GetPlayerAttackRange();
            playerStatsSO.attackRate = gameController.GetPlayerAttackRate();

            //set public variables based on SO
            damage = playerStatsSO.damage;
            attackRange = playerStatsSO.attackRange;
            attackRate = playerStatsSO.attackRate;

            playerUi.UpdateAttackUi();
        }

        private void UpdateAnimations()
        {
            playerMovement = GetComponent<PlayerMovement>(); //for animation
            animator = GetComponent<Animator>();

            animator.SetFloat("Horizontal", playerMovement.movement.x); //get movement.x direction
            animator.SetFloat("Vertical", playerMovement.movement.y); //get movement.y direction
            animator.SetTrigger("Attack"); //play atk animation based on direction
        }

        private void Attack()
        {
            UpdateAnimations();

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers); //detect enemies in range

            foreach (Collider2D enemy in hitEnemies) //damage enemies
            {
                EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

                if (enemyScript != null)
                {
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController != null)
                    {
                        gameController.DamageEnemy(enemyScript, damage);
                    }
                }
            }

            ChangeState(PlayerState.attack);

            nextAttackTime = Time.time + 1f / attackRate; //for controlling attack speed, higher attackRate = faster attack speed
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
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

        public void DoPause()
        {
            //do nothing
        }

        #endregion
    }
}
