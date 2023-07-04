using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class StartGame : MonoBehaviour
    {
        public static StartGame Instance;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        private GameController gameController;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this); //only destroy script
        }

        private void Start()
        {
            playerStatsSO.ResetStats(); //for resetting between game sessions in unity editor only

            CsvReader csvReader = FindObjectOfType<CsvReader>();
            gameController = FindObjectOfType<GameController>();

            if (csvReader != null && csvReader.playerDataList.playerData.Length > 0)
            {
                gameController.currentPlayerLevel = csvReader.playerDataList.playerData[0].level; //set currentPlayerLevel first from first line of CSV
            }
        }
    }
}
