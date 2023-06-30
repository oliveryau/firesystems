using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerMovement : PlayerScript, InputReceiver
    {
        private GameController gameController;
        private Rigidbody2D rb;
        private Animator animator;
        private float movementSpeed;

        public Vector2 movement;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController;

            movement = Vector2.zero; //set to Vector2.zero first
            animator = GetComponent<Animator>();

            currentState = PlayerState.idle; //set initial state
        }

        public void SetMovementSpeed(float speed)
        {
            movementSpeed = speed;
        }

        private void UpdateAnimationAndMovement()
        {
            rb = GetComponent<Rigidbody2D>();

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

        #endregion
    }
}
