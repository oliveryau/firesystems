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
        public float totalDamageTaken; //can hide later on
        public float totalExpGained; //can hide later on

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        public CSVWriter csvWriter;
        private GameController gameController;
        private PlayerUi playerUi;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController; //set game controller reference

            //set SO stats first
            playerStatsSO.level = gameController.GetPlayerLevel();
            playerStatsSO.maxHp = gameController.GetPlayerHp();
            playerStatsSO.movementSpeed = gameController.GetPlayerMovementSpeed();
            playerStatsSO.maxExp = gameController.GetPlayerExp();
            playerStatsSO.hp = gameController.currentPlayerHp;
            playerStatsSO.currentExp = gameController.currentPlayerExp;

            //set to initial SO stats
            playerStatsSO.initialLevel = playerStatsSO.level;
            playerStatsSO.initialHp = playerStatsSO.hp;
            playerStatsSO.initialMaxHp = playerStatsSO.maxHp;
            playerStatsSO.initialMovementSpeed = playerStatsSO.movementSpeed;
            playerStatsSO.initialCurrentExp = playerStatsSO.currentExp;
            playerStatsSO.initialMaxExp = playerStatsSO.maxExp;

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

            //set SO stats first
            playerStatsSO.level = newLevel;
            playerStatsSO.maxHp = gameController.GetPlayerHp();
            playerStatsSO.movementSpeed = gameController.GetPlayerMovementSpeed();
            playerStatsSO.maxExp = gameController.GetPlayerExp();
            playerStatsSO.hp = playerStatsSO.maxHp;
            playerStatsSO.currentExp = gameController.currentPlayerExp; //set exp to current

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
