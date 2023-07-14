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
        public PauseMenu pauseMenu;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (activeReceiver != (InputReceiver)pauseMenu)
                {
                    activeReceiver = pauseMenu;
                    activeReceiver.DoPause();
                }
            }
            else if (activeReceiver == (InputReceiver)playerMovement)
            {
                //input manager stuff
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                //apply move/attack based on active receiver
                activeReceiver.DoMove(movement); // move

                if (Time.time >= playerAttack.nextAttackTime) //if current time >= nextAttackTime
                {
                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) //attack
                    {
                        activeReceiver = playerAttack; //switch to playerAttack input
                        activeReceiver.DoAttack(); //click or spacebar to attack

                        activeReceiver = playerMovement; //switch back to playerMovement input after attacking
                    }
                }
            }            

            #region Dialogue Setups



            #endregion
        }

        public void SetInputReceiver(InputReceiver inputReceiver)
        {
            activeReceiver = inputReceiver; //for controlling only 1 input at a time
        }

        public void RemoveInputReceiver(InputReceiver inputReceiver) //for death
        {
            activeReceiver = null;
        }
    }

    public interface InputReceiver
    {
        public void DoMove(Vector2 moving); //movement

        public void DoAttack(); //attack

        public void DoPause(); //pause
    }
}
