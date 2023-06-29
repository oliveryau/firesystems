using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class GameController : MonoBehaviour
    {
        private int currentPlayerLevel;

        public InputHandler inputHandler;
        public GameObject player;

        private void Start()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                currentPlayerLevel = csvReader.playerDataList.playerData[0].level; //set currentPlayerLevel first from CSV
            }

            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
            if (playerStats != null && playerMovement != null && playerAttack != null) //initialize all methods
            {
                playerStats.Initialize(this); //set level and movement speed first

                playerMovement.Initialize(this); //initialize player movement
                SetPlayerMovementSpeed(playerMovement, playerStats.movementSpeed); //set movement speed for player movement input

                playerAttack.Initialize(this); //set up damage and range
            }

            StartGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                currentPlayerLevel += 1;

                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.UpdatePlayerStats(currentPlayerLevel); //update player stats when level up
                }

                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                if (playerAttack != null)
                {
                    playerAttack.UpdatePlayerAttackStats(); //update playerAttack stats when level up
                }
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                DamagePlayer(10); //call DamagePlayer to decrease player HP
            }
        }

        public void StartGame()
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                Debug.Log("Setting input receiver");
                inputHandler.SetInputReceiver((InputReceiver)playerScript);
            }
        }

        public int GetPlayerLevel()
        {
            return currentPlayerLevel;
        }

        public void SetPlayerMovementSpeed(PlayerMovement playerMovement, float speed)
        {
            playerMovement.SetMovementSpeed(speed);
        }

        public void DamagePlayer(float damage)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.hp -= damage; //-hp when damaged
                playerStats.SetHealthBar(playerStats.hp); //set hpBar value
            }
        }

        public void DamageEnemy(EnemyScript enemy, float damage)
        {
            enemy.hp -= damage;

            if (enemy.hp <= 0) //enemy is defeated
            {
                Destroy(enemy.gameObject); //destroy
            }
        }

        #region Player Stats CSV Retrieval

        public float GetPlayerHp() //based on currentPlayerLevel
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                foreach (var playerData in csvReader.playerDataList.playerData)
                {
                    if (playerData.level == currentPlayerLevel) //based on currentPlayerlevel
                    {
                        return playerData.hp; //get hp
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetPlayerMovementSpeed()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                foreach (var playerData in csvReader.playerDataList.playerData)
                {
                    if (playerData.level == currentPlayerLevel) //based on currentPlayerlevel
                    {
                        return playerData.movementSpeed; //get movement speed
                    }
                }
            }
            return 0f; //if nothing
        }

        #endregion

        #region Player Attack CSV Retrieval

        public float GetPlayerDamage()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerAttackDataList.playerAttackData.Length > 0)
            {
                foreach (var playerAttackData in csvReader.playerAttackDataList.playerAttackData)
                {
                    if (playerAttackData.level == currentPlayerLevel) //based on currentPlayerLevel
                    {
                        return playerAttackData.damage; //get damage
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetPlayerAttackRange() //based on level
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerAttackDataList.playerAttackData.Length > 0)
            {
                foreach (var playerAttackData in csvReader.playerAttackDataList.playerAttackData)
                {
                    if (playerAttackData.level == currentPlayerLevel) //based on currentPlayerLevel
                    {
                        return playerAttackData.attackRange; //get attack range
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetPlayerAttackRate()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerAttackDataList.playerAttackData.Length > 0)
            {
                foreach (var playerAttackData in csvReader.playerAttackDataList.playerAttackData)
                {
                    if (playerAttackData.level == currentPlayerLevel) //based on currentPlayerLevel
                    {
                        return playerAttackData.attackRate; //get attack rate
                    }
                }
            }
            return 0f; //if nothing
        }

        #endregion
    }
}
