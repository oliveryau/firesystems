using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeGuard : EnemyScript
    {
        private Transform player;
        private Rigidbody2D rb;

        [Header("Other Stats")]
        public float chaseRadius;
        public float attackRadius;
        public Transform spawnPosition;

        public override void Initialize(GameController gameController) { }

        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();

            ChangeState(EnemyState.idle); //set initial state
        }

        private void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(player.position, transform.position) <= chaseRadius
                && Vector3.Distance(player.position, transform.position) > attackRadius)
            {
                Vector3 direction = (player.position - transform.position).normalized; //calculate player and enemy vector distance, then normalize to prevent faster diagonal movement
                Vector3 velocity = direction * moveSpeed; //calculate the speed to move to the player

                rb.velocity = velocity; //set the calculation to the rigidbody for the enemy to move to the player
                ChangeState(EnemyState.move);
            }
            else
            {
                rb.velocity = Vector2.zero; //since movement by velocity, set speed back to zero when player out of range
                ChangeState(EnemyState.idle);
            }
        }
    }
}
