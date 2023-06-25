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

        public override void Initialize(GameController aController)
        {
            rb = GetComponent<Rigidbody2D>();
            movement = Vector2.zero;
        }

        #region Input Handling

        public void DoMove(Vector2 moving)
        {
            movement = moving;
            rb.velocity = movement.normalized * movementSpeed; //normalize to prevent faster diagonal movement
        }

        public void DoAttack()
        {
            //do nothing
        }

        #endregion
    }
}
