//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class ItemDrop : MonoBehaviour
    {
        public GameController gameController;

        public int enemyId;
        public string dropPrefabName;
        public string dropType;
        public float dropValue;
        public float dropPercentage;

        void Start()
        {
            if (gameController == null)
            {
                gameController = FindObjectOfType<GameController>();
            }
        }

        public void SetDropItems(int enemyId)
        {
            gameController = FindObjectOfType<GameController>();
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.itemDropDataList.itemDropData.Length > 0)
            {
                foreach (var item in csvReader.itemDropDataList.itemDropData)
                {
                    if (item.enemyId == enemyId)
                    {
                        this.enemyId = enemyId;
                        dropPrefabName = item.dropPrefabName;
                        dropType = item.dropType;
                        dropValue = item.dropValue;
                        dropPercentage = item.dropPercentage;
                    }
                }
            }
        }

        public void SpawnDropItems(int enemyId, Vector3 enemyPosition)
        {
            int randomRoll = Random.Range(1, 101); //1 - 100
            if (randomRoll <= dropPercentage)
            {
                GameObject droppedItem = Instantiate(Resources.Load<GameObject>("Prefabs/Items/" + dropPrefabName), enemyPosition, Quaternion.identity);

                // set the values first
                droppedItem.GetComponent<ItemDropManager>().dropType = dropType;
                droppedItem.GetComponent<ItemDropManager>().dropValue = dropValue;
            }
        }
    }
}
