using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class PlayerStats : PlayerScript
    {
        [Header("Stats")]
        public int level;
        public float hp;
        public float maxHp;
        public float movementSpeed;
        public float currentExp;
        public float maxExp;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        private GameController gameController;
        private PlayerUi playerUi;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController; //set game controller reference

            //set SO stats by getting from gameController
            playerStatsSO.level = gameController.GetPlayerLevel();
            playerStatsSO.maxHp = gameController.GetPlayerHp();
            playerStatsSO.movementSpeed = gameController.GetPlayerMovementSpeed();
            playerStatsSO.maxExp = gameController.GetPlayerExp();

            playerStatsSO.hp = gameController.currentPlayerHp;
            playerStatsSO.currentExp = gameController.currentPlayerExp;

            //set public variables based on SO
            level = playerStatsSO.level;
            hp = playerStatsSO.hp;
            maxHp = playerStatsSO.maxHp;
            movementSpeed = playerStatsSO.movementSpeed;
            currentExp = playerStatsSO.currentExp;
            maxExp = playerStatsSO.maxExp;
        }

        public void UpdatePlayerStats(int newLevel) //level up, update new stats
        {
            gameController = FindObjectOfType<GameController>();
            playerUi = GetComponent<PlayerUi>();

            //set SO stats by getting from gameController
            playerStatsSO.level = newLevel;
            playerStatsSO.maxHp = gameController.GetPlayerHp();
            playerStatsSO.movementSpeed = gameController.GetPlayerMovementSpeed();
            playerStatsSO.maxExp = gameController.GetPlayerExp();

            playerStatsSO.hp = playerStatsSO.maxHp;
            playerStatsSO.currentExp = 0; //set exp to 0

            //set public variables based on SO
            level = playerStatsSO.level;
            hp = playerStatsSO.hp;
            maxHp = playerStatsSO.maxHp;
            movementSpeed = playerStatsSO.movementSpeed;
            currentExp = playerStatsSO.currentExp;
            maxExp = playerStatsSO.maxExp;

            playerUi.UpdateStatsUi();
        }
    }
}
