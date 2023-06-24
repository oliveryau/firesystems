using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerMovement : PlayerScript, InputReceiver
    {
        private Rigidbody2D rb;
        private Vector2 movement;

        public float movementSpeed;

        #region Input Handling

        public override void Initialize(GameController gameController)
        {
            rb = GetComponent<Rigidbody2D>();
            movement = Vector2.zero;
        }

        public void DoMove(Vector2 moving)
        {
            movement = moving;

            if (rb != null)
            {
                rb.velocity = movement.normalized * movementSpeed; //normalize to prevent faster diagonal movement
            }
        }

        public void DoAttack()
        {
            //do nothing
        }

        #endregion
    }
}
