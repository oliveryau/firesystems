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
        public GameObject npc;

        private void Start()
        {
            activeReceiver = playerMovement; //set initial inputReceiver
        }

        private void Update()
        {
            #region Movement Setups

            //input manager stuff
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //apply move/attack based on active receiver
            activeReceiver.DoMove(movement); // move

            #endregion

            #region Attack Setups

            if (Time.time >= playerAttack.nextAttackTime) //if current time >= nextAttackTime
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) //attack
                {
                    if (activeReceiver == (InputReceiver)playerMovement)
                    {
                        activeReceiver = playerAttack; //switch to playerAttack input
                    }

                    activeReceiver.DoAttack(); //click or spacebar to attack

                    activeReceiver = playerMovement; //switch back to playerMovement input after attacking
                }
            }

            #endregion

            #region Dialogue Setups



            #endregion
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
