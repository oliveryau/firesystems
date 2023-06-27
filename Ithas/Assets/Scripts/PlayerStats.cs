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
            this.gameController = gameController; //set game controller reference

            level = gameController.GetPlayerLevel();
            movementSpeed = gameController.GetPlayerMovementSpeed();

            //base.Initialize(gameController);
        }
    }
}
