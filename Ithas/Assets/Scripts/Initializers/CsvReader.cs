//oliver, celine
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class CsvReader : MonoBehaviour
    {
        [SerializeField] private TextAsset playerDataCsv;
        [SerializeField] private TextAsset playerAttackDataCsv;
        [SerializeField] private TextAsset enemyTypeDataCsv;
        [SerializeField] private TextAsset levelEnemyDataCsv;
        [SerializeField] private TextAsset dialogueDataCsv;
        [SerializeField] private TextAsset actorDataCsv;
        [SerializeField] private TextAsset itemDropDataCsv;
        [SerializeField] private TextAsset achievementDataCsv;


        #region PlayerData Classes

        [System.Serializable]
        public class PlayerData
        {
            public int level;
            public float hp;
            public float movementSpeed;
            public float exp;
        }

        [System.Serializable]
        public class PlayerDataArray
        {
            public PlayerData[] playerData;
        }

        #endregion

        #region PlayerAttack Classes

        [System.Serializable]
        public class PlayerAttackData
        {
            public int level;
            public float damage;
            public float attackRange;
            public float attackRate;
        }

        [System.Serializable]
        public class PlayerAttackDataArray
        {
            public PlayerAttackData[] playerAttackData;
        }

        #endregion

        #region EnemyType Classes

        [System.Serializable]
        public class EnemyTypeData
        {
            public int enemyId;
            public string enemyName;
            public float hp;
            public float damage;
            public float moveSpeed;
            public float exp;
            public float chaseRadius;
            public float chaseEndRadius;
            public float attackRadius;
            public float attackRange;
            public float attackRate;
            public float attackDelay;
        }

        [System.Serializable]
        public class EnemyTypeDataArray
        {
            public EnemyTypeData[] enemyTypeData;
        }

        #endregion

        #region Level Classes

        [System.Serializable]
        public class LevelEnemyData
        {
            public int levelId;
            public int enemyNo;
            public string enemyPrefabName;
            public Vector2 homePosition;
        }

        [System.Serializable]
        public class LevelDataArray
        {
            public LevelEnemyData[] levelData;
        }

        #endregion

        #region Dialogue Classes

        [System.Serializable]
        public class DialogueData
        {
            public int actorId;
            public int cutscene;
            public int cutsceneRef;
            public int speakerLeft;
            public int speakerRight;
            public int currentSpeaker;
            public string text;
            public string choice;
        }

        [System.Serializable]
        public class DialogueDataArray
        {
            public DialogueData[] dialogueData;
        }

        #endregion

        #region ActorData Classes

        [System.Serializable]
        public class ActorData
        {
            public int actorId;
            public string actorName;
            public Sprite actorImg;
        }

        [System.Serializable]
        public class ActorDataArray
        {
            public ActorData[] actorData;
        }

        #endregion

        #region ItemDropData Classes

        [System.Serializable]
        public class ItemDropData
        {
            public int enemyId;
            public string dropPrefabName;
            public string dropType;
            public float dropValue;
            public float dropPercentage;
        }
        [System.Serializable]
        public class ItemDropDataArray
        {
            public ItemDropData[] itemDropData;
        }

        #endregion

        #region Achievement Data Classes

        [System.Serializable]
        public class AchievementData
        {
            public int achievementId;
            public string achievementName;
            public string achievementType;
            public int achievementValue;
            public string achievementDescription;
        }
        [System.Serializable]
        public class AchievementDataArray
        {
            public AchievementData[] achievementData;
        }

        #endregion


        public PlayerDataArray playerDataList = new PlayerDataArray(); //instance of playerDataArray
        public PlayerAttackDataArray playerAttackDataList = new PlayerAttackDataArray(); //instance of playerAttackDataArray
        public EnemyTypeDataArray enemyTypeDataList = new EnemyTypeDataArray(); //instance of enemyTypeDataArray
        public LevelDataArray levelDataList = new LevelDataArray(); //instance of levelDataArray
        public DialogueDataArray dialogueDataList = new DialogueDataArray(); //instance of dialogueDataArray
        public ActorDataArray actorDataList = new ActorDataArray(); //instance of ActorDataArray
        public ItemDropDataArray itemDropDataList = new ItemDropDataArray(); //instance of ItemDropDataArray
        public AchievementDataArray achievementDataList = new AchievementDataArray(); //instance of AchievementDataArray

        private void Awake()
        {
            ReadPlayerData();
            ReadPlayerAttackData();
            ReadEnemyTypeData();
            ReadLevelData();
            ReadDialogueData();
            ReadActorData();
            ReadItemDropData();
            ReadAchievementData();
        }

        private void ReadPlayerData()
        {
            string[] data = playerDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 4 - 1; //noOfColumns - headerRow
            playerDataList.playerData = new PlayerData[tableSize]; //initialize playerDataList's playerData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                playerDataList.playerData[i] = new PlayerData();

                playerDataList.playerData[i].level = int.Parse(data[4 * (i + 1)]);
                playerDataList.playerData[i].hp = float.Parse(data[4 * (i + 1) + 1]);
                playerDataList.playerData[i].movementSpeed = float.Parse(data[4 * (i + 1) + 2]);
                playerDataList.playerData[i].exp = float.Parse(data[4 * (i + 1) + 3]);
            }
        }

        private void ReadPlayerAttackData()
        {
            string[] data = playerAttackDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 4 - 1; //noOfColumns - headerRow
            playerAttackDataList.playerAttackData = new PlayerAttackData[tableSize]; //initialize playerAttackDataList's playerAttackData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                playerAttackDataList.playerAttackData[i] = new PlayerAttackData();

                playerAttackDataList.playerAttackData[i].level = int.Parse(data[4 * (i + 1)]);
                playerAttackDataList.playerAttackData[i].damage = float.Parse(data[4 * (i + 1) + 1]);
                playerAttackDataList.playerAttackData[i].attackRange = float.Parse(data[4 * (i + 1) + 2]);
                playerAttackDataList.playerAttackData[i].attackRate = float.Parse(data[4 * (i + 1) + 3]);
            }
        }

        private void ReadEnemyTypeData()
        {
            string[] data = enemyTypeDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 12 - 1; //noOfColumns - headerRow
            enemyTypeDataList.enemyTypeData = new EnemyTypeData[tableSize]; //initialize enemyTypeDataList's enemyTypeData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                enemyTypeDataList.enemyTypeData[i] = new EnemyTypeData();

                enemyTypeDataList.enemyTypeData[i].enemyId = int.Parse(data[12 * (i + 1)]);
                enemyTypeDataList.enemyTypeData[i].enemyName = (data[12 * (i + 1) + 1]);
                enemyTypeDataList.enemyTypeData[i].hp = float.Parse(data[12 * (i + 1) + 2]);
                enemyTypeDataList.enemyTypeData[i].damage = float.Parse(data[12 * (i + 1) + 3]);
                enemyTypeDataList.enemyTypeData[i].moveSpeed = float.Parse(data[12 * (i + 1) + 4]);
                enemyTypeDataList.enemyTypeData[i].exp = float.Parse(data[12 * (i + 1) + 5]);
                enemyTypeDataList.enemyTypeData[i].chaseRadius = float.Parse(data[12 * (i + 1) + 6]);
                enemyTypeDataList.enemyTypeData[i].chaseEndRadius = float.Parse(data[12 * (i + 1) + 7]);
                enemyTypeDataList.enemyTypeData[i].attackRadius = float.Parse(data[12 * (i + 1) + 8]);
                enemyTypeDataList.enemyTypeData[i].attackRange = float.Parse(data[12 * (i + 1) + 9]);
                enemyTypeDataList.enemyTypeData[i].attackRate = float.Parse(data[12 * (i + 1) + 10]);
                enemyTypeDataList.enemyTypeData[i].attackDelay = float.Parse(data[12 * (i + 1) + 11]);
            }
        }

        private void ReadLevelData()
        {
            string[] data = levelEnemyDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 4 - 1; //noOfColumns - headerRow
            levelDataList.levelData = new LevelEnemyData[tableSize]; //initialize levelDataList's levelData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                levelDataList.levelData[i] = new LevelEnemyData();

                levelDataList.levelData[i].levelId = int.Parse(data[4 * (i + 1)]);
                levelDataList.levelData[i].enemyNo = int.Parse(data[4 * (i + 1) + 1]);
                levelDataList.levelData[i].enemyPrefabName = (data[4 * (i + 1) + 2]);

                string[] homePos = data[4 * (i + 1) + 3].Split('#'); //split based on # in csv
                float x = float.Parse(homePos[0]);
                float y = float.Parse(homePos[1]);
                levelDataList.levelData[i].homePosition = new Vector2(x, y);
            }
        }

        private void ReadDialogueData()
        {
            string[] data = dialogueDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 8 - 1; //noOfColumns - headerRow
            dialogueDataList.dialogueData = new DialogueData[tableSize]; //initialize dialogueDataList's dialogueData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                dialogueDataList.dialogueData[i] = new DialogueData();

                dialogueDataList.dialogueData[i].actorId = int.Parse(data[8 * (i + 1)]);
                dialogueDataList.dialogueData[i].cutscene = int.Parse(data[8 * (i + 1) + 1]);
                dialogueDataList.dialogueData[i].cutsceneRef = int.Parse(data[8 * (i + 1) + 2]);
                dialogueDataList.dialogueData[i].speakerLeft = int.Parse(data[8 * (i + 1) + 3]);
                dialogueDataList.dialogueData[i].speakerRight = int.Parse(data[8 * (i + 1) + 4]);
                dialogueDataList.dialogueData[i].currentSpeaker = int.Parse(data[8 * (i + 1) + 5]);
                dialogueDataList.dialogueData[i].text = (data[8 * (i + 1) + 6]);
                dialogueDataList.dialogueData[i].choice = (data[8 * (i + 1) + 7]);
            }
        }

        private void ReadActorData()
        {
        #if UNITY_STANDALONE_WIN
            string[] data = actorDataCsv.text.Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        #endif
        #if UNITY_STANDALONE_OSX
            string[] data = actorDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        #endif

            int tableSize = (data.Length) / 3 - 1; //noOfColumns - headerRow
            actorDataList.actorData = new ActorData[tableSize];

            for (int i = 0; i < tableSize; i++)
            {
                actorDataList.actorData[i] = new ActorData();

                actorDataList.actorData[i].actorId = int.Parse(data[3 * (i + 1)]);
                actorDataList.actorData[i].actorName = (data[3 * (i + 1) + 1]);

                string filePath = (data[3 * (i + 1) + 2]);
                actorDataList.actorData[i].actorImg = Resources.Load<Sprite>(filePath);
            }
        }

        private void ReadItemDropData()
        {
#if UNITY_STANDALONE_WIN
            string[] data = itemDropDataCsv.text.Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
#endif
#if UNITY_STANDALONE_OSX
            string[] data = itemDropDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
#endif

            int tableSize = (data.Length) / 5 - 1; //noOfColumns - headerRow
            itemDropDataList.itemDropData = new ItemDropData[tableSize];

            for (int i = 0; i < tableSize; i++)
            {
                itemDropDataList.itemDropData[i] = new ItemDropData();

                itemDropDataList.itemDropData[i].enemyId = int.Parse(data[5 * (i + 1)]);
                itemDropDataList.itemDropData[i].dropPrefabName = (data[5 * (i + 1) + 1]);
                itemDropDataList.itemDropData[i].dropType = (data[5 * (i + 1) + 2]);
                itemDropDataList.itemDropData[i].dropValue = float.Parse(data[5 * (i + 1) + 3]);
                itemDropDataList.itemDropData[i].dropPercentage = float.Parse(data[5 * (i + 1) + 4]);
            }
        }

        private void ReadAchievementData()
        {
#if UNITY_STANDALONE_WIN
            string[] data = achievementDataCsv.text.Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
#endif
#if UNITY_STANDALONE_OSX
            string[] data = achievementDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
#endif

            int tableSize = (data.Length) / 5 - 1; //noOfColumns - headerRow
            achievementDataList.achievementData = new AchievementData[tableSize];

            for (int i = 0; i < tableSize; i++)
            {
                achievementDataList.achievementData[i] = new AchievementData();

                achievementDataList.achievementData[i].achievementId = int.Parse(data[5 * (i + 1)]);
                achievementDataList.achievementData[i].achievementName = (data[5 * (i + 1) + 1]);
                achievementDataList.achievementData[i].achievementType = (data[5 * (i + 1) + 2]);
                achievementDataList.achievementData[i].achievementValue = int.Parse(data[5 * (i + 1) + 3]);
                achievementDataList.achievementData[i].achievementDescription = (data[5 * (i + 1) + 4]);

            }

        }
    }
}