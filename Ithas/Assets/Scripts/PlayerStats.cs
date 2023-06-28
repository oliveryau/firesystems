using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ithas
{
    public class PlayerStats : PlayerScript
    {
        private GameController gameController;

        public int level;
        public float hp;
        public float maxHp;
        public float movementSpeed;

        [Header("Others")]
        public Slider hpBar;

        public override void Initialize(GameController gameController)
        {
            Debug.Log("Get base stats");
            this.gameController = gameController; //set game controller reference

            level = gameController.GetPlayerLevel();
            maxHp = gameController.GetPlayerHp();
            movementSpeed = gameController.GetPlayerMovementSpeed();

            hp = maxHp; //set current hp to maxHp

            hpBar.maxValue = maxHp; //set hpBar values
            hpBar.value = maxHp;
        }

        public void UpdatePlayerStats(int newLevel) //everytime when level up
        {
            level = newLevel;
            maxHp = gameController.GetPlayerHp();
            movementSpeed = gameController.GetPlayerMovementSpeed();

            hp = maxHp; //maxHp when level up
            SetHealthBar(maxHp);
        }

        public void SetHealthBar(float hp)
        {
            hpBar.value = hp;
        }
    }
}
