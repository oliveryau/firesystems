using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class GameController : MonoBehaviour
    {
        public InputHandler inputHandler;
        public GameObject player;

        private void Start()
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            if (playerStats != null && playerMovement != null)
            {
                playerStats.Initialize(this); //set level and movement speed first
                playerMovement.Initialize(this); //initialize player movement

                SetMovementSpeed(playerMovement, playerStats.movementSpeed); //set movement speed for player movement input
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

        public int GetPlayerLevelFromCSV()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.myPlayerLevel.player.Length > 0)
            {
                return csvReader.myPlayerLevel.player[0].level; //get level from CSV
            }

            return 0; //if nothing
        }

        public float GetMovementSpeed(int level) //based on level
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.myPlayerLevel.player.Length > 0)
            {
                foreach (var playerData in csvReader.myPlayerLevel.player)
                {
                    if (playerData.level == level) //check level
                    {
                        return playerData.moveSpeed; //get movement speed based
                    }
                }
            }

            return 0f; //if nothing
        }

        public void SetMovementSpeed(PlayerMovement playerMovement, float speed)
        {
            playerMovement.SetMovementSpeed(speed);
        }
    }
}
