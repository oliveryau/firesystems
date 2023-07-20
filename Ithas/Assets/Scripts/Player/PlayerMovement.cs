//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerMovement : PlayerScript, InputReceiver
    {
        public Vector2 movement;

        private PlayerStats playerStats;
        private Rigidbody2D rb;
        private Animator animator;
        private float movementSpeed;

        public override void Initialize(GameController gameController)
        {
            playerStats = GetComponent<PlayerStats>();
            animator = GetComponent<Animator>();

            movement = Vector2.zero; //set movement input to Vector2.zero first
            movementSpeed = playerStats.movementSpeed; //set movement speed

            currentState = PlayerState.idle; //set initial state
        }

        private void UpdateAnimationAndMovement()
        {
            playerStats = GetComponent<PlayerStats>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            movementSpeed = playerStats.movementSpeed;

            if (movement != Vector2.zero) //if movement input is read
            {
                rb.velocity = movement.normalized * movementSpeed; //normalize to prevent faster diagonal movement
                animator.SetFloat("Horizontal", movement.x); //get movement.x direction
                animator.SetFloat("Vertical", movement.y); //get movement.y direction
                animator.SetBool("Moving", true); //play moving animation based on direction

                ChangeState(PlayerState.move);
            }

            if (movement == Vector2.zero) //if movement input is not read
            {
                rb.velocity = Vector2.zero; //since movement by velocity, set speed back to zero
                animator.SetBool("Moving", false); //stop moving animation

                ChangeState(PlayerState.idle);
            }
        }

        #region Input Handling

        public void DoMove(Vector2 moving)
        {
            movement = moving;

            UpdateAnimationAndMovement();
        }

        public void DoAttack()
        {
            //do nothing
        }

        public void DoPause()
        {
            //do nothing
        }

        #endregion
    }
}
