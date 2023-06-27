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

            if (playerStats != null && playerMovement != null && playerAttack != null)
            {
                playerStats.Initialize(this); //set level and movement speed first
                playerMovement.Initialize(this); //initialize player movement

                SetMovementSpeed(playerMovement, playerStats.movementSpeed); //set movement speed for player movement input

                playerAttack.Initialize(this); //set up damage and range
            }

            StartGame();
        }

        public void StartGame()
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.Initialize(this);
                inputHandler.SetInputReceiver((InputReceiver)playerScript);
            }
        }

        public int GetPlayerLevel() //based on currentPlayerLevel
        {
            return currentPlayerLevel;
        }

        public float GetPlayerMovementSpeed(int level) //based on level
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                foreach (var playerData in csvReader.playerDataList.playerData)
                {
                    if (playerData.level == level) //check level
                    {
                        return playerData.movementSpeed; //get movement speed based
                    }
                }
            }

            return 0f; //if nothing
        }

        public void SetMovementSpeed(PlayerMovement playerMovement, float speed)
        {
            playerMovement.SetMovementSpeed(speed);
        }

        public float GetPlayerDamage() //based on level
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerAttackDataList.playerAttackData.Length > 0)
            {
                foreach (var playerAttackData in csvReader.playerAttackDataList.playerAttackData)
                {
                    if (playerAttackData.level == currentPlayerLevel) //check level
                    {
                        return playerAttackData.damage; //get damage
                    }
                }
            }

            return 0f; //if nothing
        }

        public float GetPlayerAttackRange()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerAttackDataList.playerAttackData.Length > 0)
            {
                foreach (var playerAttackData in csvReader.playerAttackDataList.playerAttackData)
                {
                    if (playerAttackData.level == currentPlayerLevel) // Check level against the current player level
                    {
                        return playerAttackData.attackRange; // Get attack range
                    }
                }
            }

            return 0f; // If nothing
        }
    }
}
