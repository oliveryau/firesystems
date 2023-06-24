using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class InputHandler : MonoBehaviour, InputReceiver
    {
        private InputReceiver activeReceiver;

        public PlayerMovement playerMovement;
        public PlayerAttack playerAttack;

        public void SetInputReceiver(InputReceiver inputReceiver)
        {
            activeReceiver = inputReceiver; //for controlling only 1 input at a time
        }

        void Update()
        {
            //input manager stuff
            Vector2 moving = Vector2.zero;
            moving.x = Input.GetAxisRaw("Horizontal");
            moving.y = Input.GetAxisRaw("Vertical");

            //apply move/attack based on active receiver
            if (activeReceiver != null)
            {
                activeReceiver.DoMove(moving);
                if (Input.GetMouseButtonDown(0))
                {
                    activeReceiver.DoAttack();
                }
            }
        }

        public void DoMove(Vector2 movement)
        {
            //do nothing
        }

        public void DoAttack()
        {
            //do nothing
        }
    }

    public interface InputReceiver
    {
        void DoMove(Vector2 action); //movement

        void DoAttack(); //attack
    }
}
