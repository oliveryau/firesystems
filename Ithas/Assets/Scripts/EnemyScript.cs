using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    //base class for all enemy scripts to inherit
    public class EnemyScript : MonoBehaviour
    {
        public Transform player;
        private bool isAttacking = false;
        private float attackStartTime = 0f;
        private float nextAttackTime = 0f; //tracker

        [Header("Stats")]
        public int enemyId;
        public string enemyName;
        public float hp;
        public float damage;
        public float moveSpeed;
        public float exp;

        [Header("Chase Stats")]
        public float chaseRadius;
        public float chaseEndRadius; //must be lesser than attackRadius
        public float attackRadius;

        [Header("Attack Stats")]
        public float attackRange;
        public float attackRate;
        public float attackDelay;

        [Header("Others")]
        public Vector2 homePosition;
        public LayerMask playerLayer;

        [Header("State Machine")]
        public EnemyState currentState;

        public void ChangeState(EnemyState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }

        public void AttackPlayer()
        {
            if (!isAttacking && Time.time >= nextAttackTime) //if not already attacking and current time >= nextAttackTime
            {
                isAttacking = true;
                attackStartTime = Time.time; //set attackStartTime to current time
            }

            if (isAttacking && Time.time >= (attackStartTime + attackDelay)) //isAttacking and current time >= (current time + attackDelay)
            {
                Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer); //detect player in range

                if (hitPlayer != null) //damage player
                {
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController != null)
                    {
                        gameController.DamagePlayer(damage);
                    }
                }

                ChangeState(EnemyState.attack);

                nextAttackTime = Time.time + 1f / attackRate; //for controlling attack speed, higher attackRate = faster attack speed
                isAttacking = false;
            }
        }
    }

    public enum EnemyState
    {
        idle,
        move,
        attack
    }
}
