//oliver, celine
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
        [HideInInspector] public int currentEnemyNo;
        [HideInInspector] public int enemiesKilled;
        [HideInInspector] public int objectsDestroyed;
        [HideInInspector] public Vector3 enemyPosition;

        [Header("Others")]
        public GameOverMenu gameOverMenu;
        public CSVWriter csvWriter;
        public AchievementsMenu achievementsMenu;

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

            currentEnemyNo = 1;

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

            achievementsMenu.InitializeMenu();
        }

        private void Update()
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();

            if (currentPlayerLevel < 10) //max level is 10
            {
                if (playerStats.currentExp >= playerStats.maxExp) //level up
                {
                    currentPlayerLevel += 1; //+1 player level
                    currentPlayerExp = playerStats.currentExp - playerStats.maxExp; //reset current exp with difference

                    if (playerStats != null)
                    {
                        playerStats.UpdatePlayerStats(currentPlayerLevel); //update player stats when level up
                    }

                    if (playerAttack != null)
                    {
                        playerAttack.UpdatePlayerAttackStats(); //update playerAttack stats when level up
                    }

                    achievementsMenu.UpdateAchievements("Player");
                }
                else if (Input.GetKeyDown(KeyCode.F1)) //cheat code F1 to level up
                {
                    currentPlayerLevel += 1; //+1 player level
                    currentPlayerExp = 0; //reset to 0

                    if (playerStats != null)
                    {
                        playerStats.UpdatePlayerStats(currentPlayerLevel); //update player stats when level up
                    }

                    if (playerAttack != null)
                    {
                        playerAttack.UpdatePlayerAttackStats(); //update playerAttack stats when level up
                    }

                    achievementsMenu.UpdateAchievements("Player");
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
                playerStats.totalDamageTaken += damage; //for damage analytics
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
                csvWriter.WriteCsv();
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
            enemyPosition = enemyScript.transform.position; // get the enemy position before destroying it
            ItemDrop itemDrop = enemyScript.GetComponent<ItemDrop>();
            if (itemDrop != null)
            {
                itemDrop.SpawnDropItems(enemyScript.enemyId, enemyPosition);
            }

            Destroy(enemyScript.gameObject); //destroy enemy when die

            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerUi playerUi = player.GetComponent<PlayerUi>();
            if (playerStats != null && currentPlayerLevel < 10)
            {
                playerStats.currentExp += enemyScript.exp; //add enemy's exp to player
                playerStatsSO.currentExp = playerStats.currentExp; //set it to playerStatsSO
                playerUi.SetExpBar(playerStats.currentExp); //set exp value of player
                playerStats.totalExpGained += playerStats.currentExp;
            }
            enemiesKilled++;
            achievementsMenu.UpdateAchievements("Kill");
        }

        public void DamageObject(EnemyScript enemyScript, float damage)
        {
            enemyScript.hp -= damage; //-hp when damaged
            enemyScript.UpdateEnemyHpBar(damage); //ui

            if (enemyScript.hp <= 0)
            {
                ObjectDie(enemyScript);
            }
        }

        private void ObjectDie(EnemyScript enemyScript)
        {
            Destroy(enemyScript.gameObject); //destroy object when die

            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            PlayerUi playerUi = player.GetComponent<PlayerUi>();
            if (playerStats != null && currentPlayerLevel < 10)
            {
                playerStats.currentExp += enemyScript.exp; //add enemy's exp to player
                playerStatsSO.currentExp = playerStats.currentExp; //set it to playerStatsSO
                playerUi.SetExpBar(playerStats.currentExp); //set exp value of player
                playerStats.totalExpGained += playerStats.currentExp;
            }
            objectsDestroyed++;
            achievementsMenu.UpdateAchievements("Destroy");
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
                foreach (var enemyData in csvReader.enemyTypeDataList.enemyTypeData) //enemyTypeDataList.Find(x=>x.enemyId == enemyScript.enemyId);
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

        #region Level Enemy Data CSV Retrieval

        public string GetEnemyPrefabName(StartLevel startLevel)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.levelDataList.levelData.Length > 0)
            {
                foreach (var levelEnemyData in csvReader.levelDataList.levelData)
                {
                    if (levelEnemyData.levelId == startLevel.levelId && levelEnemyData.enemyNo == currentEnemyNo)
                    {
                        return levelEnemyData.enemyPrefabName; //get enemyPrefabName
                    }
                }
            }
            return " "; //if nothing
        }

        public Vector2 GetEnemyHomePosition(StartLevel startLevel)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.levelDataList.levelData.Length > 0)
            {
                foreach (var levelEnemyData in csvReader.levelDataList.levelData)
                {
                    if (levelEnemyData.levelId == startLevel.levelId && levelEnemyData.enemyNo == currentEnemyNo)
                    {
                        return levelEnemyData.homePosition; //get enemyHomePos
                    }
                }
            }
            return Vector2.zero; //if nothing
        }

        #endregion

        #region Dialogue Data CSV Retrieval

        public Message[] GetDialogueMessages() 
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.dialogueDataList.dialogueData.Length > 0)
            {
                List<Message> messages = new List<Message>();
                foreach (var dialogueData in csvReader.dialogueDataList.dialogueData)
                {
                    messages.Add(new Message { actorId = dialogueData.actorId, message = dialogueData.text, cutscene = dialogueData.cutscene,
                        cutsceneRef = dialogueData.cutsceneRef, speakerLeft = dialogueData.speakerLeft, speakerRight = dialogueData.speakerRight,
                        currentSpeaker = dialogueData.currentSpeaker, choice = dialogueData.choice});
                }
                return messages.ToArray(); //get all messages
            }
            return null; // if no dialogue messages are found or CSV reader is not present
        }

        #endregion

        #region Actor Data CSV Retrieval

        public string GetActorName(int actorId) 
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.actorDataList.actorData.Length > 0)
            {
                foreach (var actorData in csvReader.actorDataList.actorData)
                {
                    if (actorData.actorId == actorId)
                    {
                        return actorData.actorName; // get name
                    }
                }
            }
            return ""; // if nothing
        }

        public Sprite GetActorSprite(int actorId)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.actorDataList.actorData.Length > 0)
            {
                foreach (var actorData in csvReader.actorDataList.actorData)
                {
                    if (actorData.actorId == actorId)
                    {
                        return actorData.actorImg; // get sprite
                    }
                }
            }
            return null; // if nothing
        }

        #endregion

        #region Item Drop Data CSV Retrieval
        public string GetItemDropPrefabName(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.itemDropDataList.itemDropData.Length > 0)
            {
                foreach (var dropData in csvReader.itemDropDataList.itemDropData)
                {
                    if (dropData.enemyId == enemyScript.enemyId)
                    {
                        return dropData.dropPrefabName;
                    }
                }
            }
            return null; // if nothing
        }

        public string GetItemDropType(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.itemDropDataList.itemDropData.Length > 0)
            {
                foreach (var dropData in csvReader.itemDropDataList.itemDropData)
                {
                    if (dropData.enemyId == enemyScript.enemyId)
                    {
                        return dropData.dropType;
                    }
                }
            }
            return null; // if nothing
        }

        public float GetItemDropValue(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.itemDropDataList.itemDropData.Length > 0)
            {
                foreach (var dropData in csvReader.itemDropDataList.itemDropData)
                {
                    if (dropData.enemyId == enemyScript.enemyId)
                    {
                        return dropData.dropValue;
                    }
                }
            }
            return 0; // if nothing
        }

        public float GetItemDropPercentage(EnemyScript enemyScript)
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.itemDropDataList.itemDropData.Length > 0)
            {
                foreach (var dropData in csvReader.itemDropDataList.itemDropData)
                {
                    if (dropData.enemyId == enemyScript.enemyId)
                    {
                        return dropData.dropPercentage;
                    }
                }
            }
            return 0; // if nothing
        }

        #endregion

        #region Achievement Data CSV Retrieval

        public Achievement[] GetAchievement()
        {
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.achievementDataList.achievementData.Length > 0)
            {
                List<Achievement> achievements = new List<Achievement>();
                foreach (var achievementData in csvReader.achievementDataList.achievementData)
                {
                    achievements.Add(new Achievement
                    {
                        achievementId = achievementData.achievementId,
                        achievementName = achievementData.achievementName,
                        achievementType = achievementData.achievementType,
                        achievementValue = achievementData.achievementValue,
                        achievementDescription = achievementData.achievementDescription
                    });
                }
                return achievements.ToArray(); //get all achievements
            }
            return null; // if no achievement found or CSV reader is not present
        }


        #endregion
    }
}
