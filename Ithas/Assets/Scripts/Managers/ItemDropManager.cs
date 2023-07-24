//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class ItemDropManager : MonoBehaviour
    {
        private PlayerStats playerStats;
        private PlayerUi playerUi;

        [Header("Drop Stats")]
        public string dropType;
        public float dropValue;

        [Header("Player Stats SO")]
        public PlayerStatsSO playerStatsSO;

        private void Start()
        {
            playerStats = FindObjectOfType<PlayerStats>();
            playerUi = FindObjectOfType<PlayerUi>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                switch (dropType)
                {
                    case "EXP":
                        playerStats.currentExp += dropValue;
                        //dont set exp as logic is implemented in gameController already

                        playerStatsSO.currentExp = playerStats.currentExp; //set SO
                        playerUi.SetExpBar(playerStats.currentExp); //update ui
                        break;
                    case "HP":
                        playerStats.hp += dropValue;
                        if (playerStats.hp >= playerStats.maxHp)
                        {
                            playerStats.hp = playerStats.maxHp; //set again to make sure it does not exceed
                        }

                        playerStatsSO.hp = playerStats.hp; //set SO
                        playerUi.SetHealthBar(playerStats.hp); //update ui
                        break;
                    default:
                        Debug.Log("Null");
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
}
