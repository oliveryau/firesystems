using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeGuard : EnemyScript
    {
        private GameController gameController;

        private Animator animator;
        private Rigidbody2D rb;

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;

            enemyId = 1; //set what type of enemy
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                enemyName = gameController.GetEnemyName(this);
                hp = gameController.GetEnemyHp(this);
                damage = gameController.GetEnemyDamage(this);
                moveSpeed = gameController.GetEnemyMoveSpeed(this);
                exp = gameController.GetEnemyExp(this);
                chaseRadius = gameController.GetEnemyChaseRadius(this);
                chaseEndRadius = gameController.GetEnemyChaseEndRadius(this);
                attackRadius = gameController.GetEnemyAttackRadius(this);
                attackRange = gameController.GetEnemyAttackRange(this);
                attackRate = gameController.GetEnemyAttackRate(this);
                attackDelay = gameController.GetEnemyAttackDelay(this);
                homePosition = gameController.GetEnemyHomePosition(this);
            }

            ChangeState(EnemyState.idle); //set initial state
        }

        private void Update()
        {
            UpdateAnimationsAndMovementAttack();
        }

        private void UpdateAnimationsAndMovementAttack()
        {
            if (Vector3.Distance(player.position, transform.position) <= chaseRadius
                && Vector3.Distance(player.position, transform.position) > chaseEndRadius) //chase player within chaseRadius but stop chasing at chaseEndRadius
            {
                Vector3 direction = (player.position - transform.position).normalized; //calculate player and enemy vector distance, then normalize to prevent faster diagonal movement
                Vector3 velocity = direction * moveSpeed; //calculate the speed to move to the player

                rb.velocity = velocity; //set the speed calculation for the rigidbody for the enemy to move to the player
                ChangeState(EnemyState.move);

                animator.SetFloat("Horizontal", velocity.x); //get velocity.x direction
                animator.SetFloat("Vertical", velocity.y); //get velocity.y direction
                animator.SetBool("Moving", true); //play moving animation based on direction
            }
            else if (Vector3.Distance(player.position, transform.position) <= attackRadius) //player within attackRadius
            {
                rb.velocity = Vector2.zero; //since movement by velocity, set speed back to zero when player out of range
                ChangeState(EnemyState.idle);

                animator.SetBool("Moving", false); //stop moving animation

                AttackPlayer();
            }

            if (Vector3.Distance(player.position, transform.position) > chaseRadius) //player out of range
            {
                rb.velocity = Vector2.zero; //since movement by velocity, set speed back to zero when player out of range
                ChangeState(EnemyState.idle);

                animator.SetBool("Moving", false); //stop moving animation
            }
        }
    }
}
