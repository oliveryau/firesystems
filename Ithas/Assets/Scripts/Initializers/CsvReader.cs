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
        [SerializeField] private TextAsset levelDataCsv;
        [SerializeField] private TextAsset dialogueDataCsv;

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
        public class LevelData
        {
            public int levelId;
            public int enemyNo;
            public string enemyPrefabName;
            public Vector2 homePosition;
        }

        [System.Serializable]
        public class LevelDataArray
        {
            public LevelData[] levelData;
        }

        #endregion

        #region Dialogue Classes

        [System.Serializable]
        public class DialogueData
        {
            public int dialogueId;
            public int cutscene;
            public int cutsceneRef;
            public string speakerLeft;
            public string speakerRight;
            public string currentSpeaker;
            public string text;
            public string choice;
        }

        [System.Serializable]
        public class DialogueDataArray
        {
            public DialogueData[] dialogueData;
        }

        #endregion

        public PlayerDataArray playerDataList = new PlayerDataArray(); //instance of playerDataArray
        public PlayerAttackDataArray playerAttackDataList = new PlayerAttackDataArray(); //instance of playerAttackDataArray
        public EnemyTypeDataArray enemyTypeDataList = new EnemyTypeDataArray(); //instance of enemyTypeDataArray
        public LevelDataArray levelDataList = new LevelDataArray(); //instance of levelDataArray
        public DialogueDataArray dialogueDataList = new DialogueDataArray(); //instance of dialogueDataArray

        private void Awake()
        {
            ReadPlayerData();
            ReadPlayerAttackData();
            ReadEnemyTypeData();
            ReadLevelData();
            ReadDialogueData();
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
            string[] data = levelDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 4 - 1; //noOfColumns - headerRow
            levelDataList.levelData = new LevelData[tableSize]; //initialize levelDataList's levelData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                levelDataList.levelData[i] = new LevelData();

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

                dialogueDataList.dialogueData[i].dialogueId = int.Parse(data[8 * (i + 1)]);
                dialogueDataList.dialogueData[i].cutscene = int.Parse(data[8 * (i + 1) + 1]);
                dialogueDataList.dialogueData[i].cutsceneRef = int.Parse(data[8 * (i + 1) + 2]);
                dialogueDataList.dialogueData[i].speakerLeft = (data[8 * (i + 1) + 3]);
                dialogueDataList.dialogueData[i].speakerRight = (data[8 * (i + 1) + 4]);
                dialogueDataList.dialogueData[i].currentSpeaker = (data[8 * (i + 1) + 5]);
                dialogueDataList.dialogueData[i].text = (data[8 * (i + 1) + 6]);
                dialogueDataList.dialogueData[i].choice = (data[8 * (i + 1) + 7]);
            }
        }
    }

}
