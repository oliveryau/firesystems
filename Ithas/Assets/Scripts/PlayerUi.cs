using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class PlayerUi : PlayerScript
    {
        public Slider hpBar;
        public TextMeshProUGUI hpText;
        public Slider expBar;
        public TextMeshProUGUI expText;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI damageText;

        private GameController gameController;
        private PlayerStats playerStats;
        private PlayerAttack playerAttack;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController;

            playerStats = GetComponent<PlayerStats>();
            playerAttack = GetComponent<PlayerAttack>();

            hpBar.maxValue = playerStats.maxHp; //set hpBar values
            SetHealthBar(playerStats.maxHp);

            expBar.maxValue = playerStats.maxExp; //set expBar values
            SetExpBar(playerStats.currentExp);

            moveSpeedText.text = "Speed: " + playerStats.movementSpeed.ToString(); //movespeed ui

            damageText.text = "Damage: " + playerAttack.damage.ToString(); //attack ui
        }

        public void UpdateStatsUi()
        {
            playerStats = GetComponent<PlayerStats>();

            hpBar.maxValue = playerStats.maxHp; //hpBar
            SetHealthBar(playerStats.maxHp);

            expBar.maxValue = playerStats.maxExp; //expBar
            SetExpBar(playerStats.currentExp);

            moveSpeedText.text = "Speed: " + playerStats.movementSpeed.ToString(); //movespeed ui
        }

        public void UpdateAttackUi()
        {
            playerAttack = GetComponent<PlayerAttack>();

            damageText.text = "Damage: " + playerAttack.damage.ToString(); //attack ui
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
