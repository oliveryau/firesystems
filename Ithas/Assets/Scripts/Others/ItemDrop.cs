using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ithas.CsvReader;

namespace Ithas
{
    public class ItemDrop : MonoBehaviour
    {
        public GameController gameController;
        public List<ItemDropData> dropItems = new List<ItemDropData>();

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

        public void DropItems(int enemyId, Vector3 enemyPosition)
        {
            foreach (var item in dropItems)
            {
                if (item.enemyId == enemyId && Random.value <= item.dropPercentage)
                {
                    GameObject droppedItem = Instantiate(Resources.Load<GameObject>("Prefabs/Items/" + item.dropPrefabName), enemyPosition, Quaternion.identity);
                }
            }
        }
    }
}
