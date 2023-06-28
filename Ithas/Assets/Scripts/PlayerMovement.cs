using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerMovement : PlayerScript, InputReceiver
    {
        private Animator animator;
        private Rigidbody2D rb;
        private float movementSpeed;

        public Vector2 movement;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Setting up movement");
            movement = Vector2.zero; //set to Vector2.zero first
            animator = GetComponent<Animator>();
        }

        public void SetMovementSpeed(float speed)
        {
            movementSpeed = speed;
        }

        #region Input Handling

        public void DoMove(Vector2 moving)
        {
            movement = moving;

            rb = GetComponent<Rigidbody2D>();
            rb.velocity = movement.normalized * movementSpeed; //normalize to prevent faster diagonal movement

            animator.SetFloat("Horizontal", movement.x); //check movement.x
            animator.SetFloat("Vertical", movement.y); //check movement.y
            animator.SetFloat("Speed", movement.sqrMagnitude); //check speed with sqrMagnitude
        }

        public void DoAttack()
        {
            //do nothing
        }

        #endregion
    }
}
