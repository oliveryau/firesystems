using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerMovement : PlayerScript, InputReceiver
    {
        private Rigidbody2D rb;
        private Vector2 movement;
        private float movementSpeed;

        public bool facingRight = true;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Movement initialized");
            movement = Vector2.zero; //set to Vector2.zero first
        }

        public void SetMovementSpeed(float speed)
        {
            movementSpeed = speed;
        }

        public void Flip()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }

        #region Input Handling

        public void DoMove(Vector2 moving)
        {
            movement = moving;

            rb = GetComponent<Rigidbody2D>();
            rb.velocity = movement.normalized * movementSpeed; //normalize to prevent faster diagonal movement
        }

        public void DoAttack()
        {
            //do nothing
        }

        #endregion
    }
}
