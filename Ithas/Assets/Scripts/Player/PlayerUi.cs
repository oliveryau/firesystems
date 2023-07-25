//oliver
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
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI attackRangeText;
        public TextMeshProUGUI attackRateText;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        private GameController gameController;
        private PlayerStats playerStats;
        private PlayerAttack playerAttack;

        public override void Initialize(GameController gameController)
        {
            this.gameController = gameController;

            hpBar.maxValue = playerStatsSO.maxHp; //set hpBar values
            hpBar.value = playerStatsSO.hp; //for scene switching
            hpText.text = "HP: " + playerStatsSO.hp.ToString() + "/" + playerStatsSO.maxHp.ToString();

            expBar.maxValue = playerStatsSO.maxExp; //set expBar values
            SetExpBar(playerStatsSO.currentExp);

            levelText.text = "Level: " + gameController.currentPlayerLevel.ToString(); //level ui

            moveSpeedText.text = "Speed: " + playerStatsSO.movementSpeed.ToString(); //movespeed ui

            damageText.text = "Damage: " + playerStatsSO.damage.ToString(); //attack ui
            attackRangeText.text = "Atk Range: " + playerStatsSO.attackRange.ToString();
            attackRateText.text = "Atk Rate: " + playerStatsSO.attackRate.ToString();
        }

        public void UpdateStatsUi()
        {
            playerStats = GetComponent<PlayerStats>();

            hpBar.maxValue = playerStats.maxHp; //hpBar
            SetHealthBar(playerStats.maxHp);

            expBar.maxValue = playerStats.maxExp; //expBar
            SetExpBar(playerStats.currentExp);

            levelText.text = "Level: " + gameController.currentPlayerLevel.ToString(); //level ui

            moveSpeedText.text = "Speed: " + playerStats.movementSpeed.ToString(); //movespeed ui
        }

        public void UpdateAttackUi()
        {
            playerAttack = GetComponent<PlayerAttack>();

            damageText.text = "Damage: " + playerAttack.damage.ToString(); //attack ui
            attackRangeText.text = "Atk Range: " + playerAttack.attackRange.ToString();
            attackRateText.text = "Atk Rate: " + playerAttack.attackRate.ToString();
        }

        public void SetHealthBar(float hp)
        {
            hpBar.value = hp;
            hpText.text = "HP: " + hp.ToString() + "/" + playerStatsSO.maxHp.ToString();

            if (hp <= 0) //player death
            {
                gameController.PlayerDie();
            }
        }

        public void SetExpBar(float exp)
        {
            expBar.value = exp;
            expText.text = "EXP: " + exp.ToString() + "/" + playerStatsSO.maxExp.ToString();
        }
    }

}
