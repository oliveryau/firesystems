//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.sceneLoaded += OnSceneLoaded; //subscribe to OnSceneLoaded()
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; //unsubscribe from OnSceneLoaded()
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "StartMenu")
            {
                Destroy(this.gameObject); //destroy the script if go to startMenu
            }
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
