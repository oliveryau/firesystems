using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class PlayerStats : PlayerScript
    {
        private GameController gameController;

        [Header("Stats")]
        public int level;
        public float hp;
        public float maxHp;
        public float movementSpeed;
        public float currentExp;
        public float maxExp;

        [Header("Others")]
        public Slider hpBar;
        public TextMeshProUGUI hpText;
        public Slider expBar;
        public TextMeshProUGUI expText;
        public TextMeshProUGUI moveSpeedText;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController; //set game controller reference

            level = gameController.GetPlayerLevel();
            maxHp = gameController.GetPlayerHp();
            movementSpeed = gameController.GetPlayerMovementSpeed();
            maxExp = gameController.GetPlayerExp();

            hp = maxHp; //set hp to maxHp
            hpBar.maxValue = maxHp; //set hpBar values
            SetHealthBar(maxHp);

            currentExp = 0; //set currentExp to 0
            expBar.maxValue = maxExp;
            SetExpBar(currentExp);

            moveSpeedText.text = "Speed: " + movementSpeed.ToString();
        }

        public void UpdatePlayerStats(int newLevel) //level up, update new stats
        {
            level = newLevel;
            maxHp = gameController.GetPlayerHp();
            movementSpeed = gameController.GetPlayerMovementSpeed();
            maxExp = gameController.GetPlayerExp();

            hp = maxHp; //maxHp when level up
            hpBar.maxValue = maxHp;
            SetHealthBar(maxHp);

            currentExp = 0; //reset back to 0 exp
            expBar.maxValue = maxExp;
            SetExpBar(currentExp);

            moveSpeedText.text = "Speed: " + movementSpeed.ToString();
        }

        public void SetHealthBar(float hp)
        {
            hpBar.value = hp;
            hpText.text = "HP: " + hp.ToString();

            if (hp <= 0) //player death
            {
                gameController.PlayerDie();
            }
        }

        public void SetExpBar(float exp)
        {
            expBar.value = exp;
            expText.text = "EXP: " + exp.ToString();
        }
    }
}
