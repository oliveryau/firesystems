using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class GameController : MonoBehaviour
    {
        public InputHandler inputHandler;
        public GameObject player;
        [HideInInspector] public int currentPlayerLevel;
        [HideInInspector] public float currentPlayerHp;
        [HideInInspector] public float currentPlayerExp;

        [Header("Others")]
        public GameOverMenu gameOverMenu;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        private void Start()
        {
            if (playerStatsSO != null)
            {
                currentPlayerLevel = playerStatsSO.level;
                currentPlayerHp = playerStatsSO.hp;
                currentPlayerExp = playerStatsSO.currentExp; //get from SO
            }

            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
            PlayerUi playerUi = player.GetComponent<PlayerUi>();
            if (playerStats != null && playerMovement != null && playerAttack != null && playerUi != null) //initialize all methods
            {
                playerStats.Initialize(this); //set level and movement speed first
                playerMovement.Initialize(this); //initialize player movement
                playerAttack.Initialize(this); //set up damage and range
                playerUi.Initialize(this); //set up ui
            }

            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                inputHandler.SetInputReceiver(player.GetComponent<PlayerMovement>());
            }
        }

        private void Update()
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (currentPlayerLevel < 10) //max level is 10
            {
                if (playerStats.currentExp >= playerStats.maxExp || Input.GetKeyDown(KeyCode.F1)) //cheat code: F1 key to level up  
                {
                    currentPlayerLevel += 1; //+1 player level
                    currentPlayerExp = 0; //reset current exp

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
            }
        }

        public int GetPlayerLevel()
        {
            return currentPlayerLevel;
        }

        public void DamagePlayer(float damage)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerUi playerUi = player.GetComponent<PlayerUi>();
            if (playerStats != null)
            {
                playerStats.hp -= damage; //-hp when damaged
                playerStatsSO.hp = playerStats.hp; //set it to playerStatsSO
                playerUi.SetHealthBar(playerStats.hp); //set hpBar value
            }
        }

        public void PlayerDie()
        {
            Animator playerAnim = player.GetComponent<Animator>();
            if (playerAnim != null)
            {
                playerAnim.SetBool("Death", true);
                inputHandler.RemoveInputReceiver(player.GetComponent<PlayerMovement>());
                gameOverMenu.GameOver();
            }

            playerStatsSO.ResetToInitialStats();
        }

        public void DamageEnemy(EnemyScript enemyScript, float damage)
        {
            enemyScript.hp -= damage; //-hp when damaged
            enemyScript.UpdateEnemyHpBar(damage); //ui

            if (enemyScript.hp <= 0)
            {
                EnemyDie(enemyScript);
            }
        }

        private void EnemyDie(EnemyScript enemyScript)
        {
            Destroy(enemyScript.gameObject); //destroy enemy when die

            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerUi playerUi = player.GetComponent<PlayerUi>();
            if (playerStats != null && currentPlayerLevel < 10)
            {
                playerStats.currentExp += enemyScript.exp; //add enemy's exp to player
                playerStatsSO.currentExp = playerStats.currentExp; //set it to playerStatsSO
                playerUi.SetExpBar(playerStats.currentExp); //set exp value of player
            }
        }

        #region Player Data CSV Retrieval

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

        public float GetPlayerExp()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                foreach (var playerData in csvReader.playerDataList.playerData)
                {
                    if (playerData.level == currentPlayerLevel) //based on currentPlayerlevel
                    {
                        return playerData.exp; //get movement speed
                    }
                }
            }
            return 0f; //if nothing
        }

        #endregion

        #region Player Attack Data CSV Retrieval

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

        #region Enemy Type Data CSV Retrieval

        public string GetEnemyName(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.enemyName; //get enemyName
                    }
                }
            }
            return " "; //if nothing
        }

        public float GetEnemyHp(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.hp; //get hp
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyDamage(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.damage; //get damage
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyMoveSpeed(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.moveSpeed; //get moveSpeed
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyExp(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.exp; //get exp
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyChaseRadius(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.chaseRadius; //get chaseRadius
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyChaseEndRadius(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.chaseEndRadius; //get chaseEndRadius
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyAttackRadius(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.attackRadius; //get attackRadius
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyAttackRange(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.attackRange; //get attackRange
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyAttackRate(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.attackRate; //get attackRate
                    }
                }
            }
            return 0f; //if nothing
        }

        public float GetEnemyAttackDelay(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData)
                {
                    if (enemyData.enemyId == enemyScript.enemyId) //based on enemy id
                    {
                        return enemyData.attackDelay; //get attackDelay
                    }
                }
            }
            return 0f; //if nothing
        }

        #endregion
    }
}
