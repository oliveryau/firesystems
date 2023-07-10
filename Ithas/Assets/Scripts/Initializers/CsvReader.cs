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
        [SerializeField] private TextAsset characterDataCsv;

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

        #region Character Classes

        [System.Serializable]
        public class CharacterData
        {
            public int characterId;
            public string fullName;
            public Sprite portrait;
        }

        [System.Serializable]
        public class CharacterDataArray
        {
            public CharacterData[] characterData;
        }

        #endregion

        public PlayerDataArray playerDataList = new PlayerDataArray(); //instance of playerDataArray
        public PlayerAttackDataArray playerAttackDataList = new PlayerAttackDataArray(); //instance of playerAttackDataArray
        public EnemyTypeDataArray enemyTypeDataList = new EnemyTypeDataArray(); //instance of enemyTypeDataArray
        public LevelDataArray levelDataList = new LevelDataArray(); //instance of levelDataArray
        public CharacterDataArray characterDataList = new CharacterDataArray(); //instance of characterDataArray

        List<Dialogue> dialogues = new List<Dialogue>();

        private void Awake()
        {
            ReadPlayerData();
            ReadPlayerAttackData();
            ReadEnemyTypeData();
            ReadLevelData();
            ReadCharacterData();
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

                //string[] homePos = data[13 * (i + 1) + 12].Split('#'); //split with #
                //float x = float.Parse(homePos[0]);
                //float y = float.Parse(homePos[1]);
                //enemyTypeDataList.enemyTypeData[i].homePosition = new Vector2(x, y);
            }
        }

        private void ReadLevelData()
        {
            string[] data = levelDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 4 - 1; //noOfColumns - headerRow
            levelDataList.levelData = new LevelData[tableSize]; //initialize playerDataList's playerData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                levelDataList.levelData[i] = new LevelData();

                levelDataList.levelData[i].levelId = int.Parse(data[4 * (i + 1)]);
                levelDataList.levelData[i].enemyNo = int.Parse(data[4 * (i + 1) + 1]);
                levelDataList.levelData[i].enemyPrefabName = (data[4 * (i + 1) + 2]);

                string[] homePos = data[4 * (i + 1) + 3].Split('#'); //split with #
                float x = float.Parse(homePos[0]);
                float y = float.Parse(homePos[1]);
                levelDataList.levelData[i].homePosition = new Vector2(x, y);
            }
        }

        private void ReadCharacterData()
        {
            string[] data = characterDataCsv.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = (data.Length) / 3 - 1; //noOfColumns - headerRow
            characterDataList.characterData = new CharacterData[tableSize]; //initialize characterDataList's characterData with an array of tableSize

            for (int i = 0; i < tableSize; i++)
            {
                characterDataList.characterData[i] = new CharacterData();

                characterDataList.characterData[i].characterId = int.Parse(data[3 * (i + 1)]);
                characterDataList.characterData[i].fullName = (data[3 * (i + 1) + 1]);
                //characterDataList.characterData[i].portrait = (data[3 * (i + 1) + 2]);

                string imagePath = "Art Assets/Characters/" + data[3 * (i + 1) + 2];
                characterDataList.characterData[i].portrait = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(imagePath);

                //string imagePath = "Art Assets/Characters/" + characterDataList.characterData[i].portrait;
                //characterDataList.characterData[i].portraitSprite = Resources.Load<Sprite>(characterDataList.characterData[i].portrait);
            }
        }

        private void ReadDialogueData()
        {
            TextAsset dialogueData = Resources.Load<TextAsset>("DialogueData");

            string[] data = dialogueData.text.Split(new char[] { '\n' });

            for (int i = 1; i < data.Length -1; i++)
            {
                string[] row = data[i].Split(new char[] { ',' });

                if (row[1] != "")
                {
                    Dialogue d = new Dialogue();

                    int.TryParse(row[0], out d.dialogueId);
                    int.TryParse(row[1], out d.cutscene);
                    int.TryParse(row[2], out d.cutsceneRef);
                    d.speakerLeft = row[3];
                    d.speakerRight = row[4];
                    d.currentSpeaker = row[5];
                    d.text = row[6];
                    d.choice = row[7];

                    dialogues.Add(d);
                }
            }
            foreach (Dialogue d in dialogues)
            {
                Debug.Log(d.cutscene + "," + d.currentSpeaker);
            }
        }

        
    }

}
