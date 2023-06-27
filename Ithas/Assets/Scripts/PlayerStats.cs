using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class PlayerStats : PlayerScript
    {
        public int level;
        public float movementSpeed;

        private GameController gameController;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Stats Initialized");
            this.gameController = gameController;

            level = gameController.GetPlayerLevelFromCSV();
            movementSpeed = gameController.GetMovementSpeed(level);

            base.Initialize(gameController);

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                gameController.SetMovementSpeed(playerMovement, movementSpeed);
            }
        }
    }
}
