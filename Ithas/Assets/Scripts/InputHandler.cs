using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class InputHandler : MonoBehaviour
    {
        private InputReceiver activeReceiver;
        private Vector2 movement;

        public PlayerMovement playerMovement;
        public PlayerAttack playerAttack;

        private void Start()
        {
            activeReceiver = playerMovement; //set initial inputReceiver
        }

        private void Update()
        {
            //input manager stuff
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //apply move/attack based on active receiver
            activeReceiver.DoMove(movement); // move

            if (movement.x < 0 && playerMovement.facingRight) //moving left but facing right
            {
                playerMovement.Flip();
            }
            if (movement.x > 0 && !playerMovement.facingRight) //moving right but facing left
            {
                playerMovement.Flip();
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
            {
                if (activeReceiver == (InputReceiver)playerMovement)
                {
                    activeReceiver = playerAttack; //switch to playerAttack input
                }

                activeReceiver.DoAttack(); //click or Z to attack

                activeReceiver = playerMovement; //switch back to playerMovement input after attacking
            }
        }
        public void SetInputReceiver(InputReceiver inputReceiver)
        {
            activeReceiver = inputReceiver; //for controlling only 1 input at a time
        }
    }

    public interface InputReceiver
    {
        public void DoMove(Vector2 moving); //movement

        public void DoAttack(); //attack
    }
}
