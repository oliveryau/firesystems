//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class StartLevel : MonoBehaviour
    {
        public GameObject startLevelScreen;
        public GameObject inputHandler;
        public int levelId;

        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Level")
            {
                levelId = 1;
            }
            //else if (currentScene.name == "Level 2")
            //{
            //    levelId = 2;
            //}

            GameObject enemyManager = new GameObject("EnemyManager");
            EnemyScript enemyScript = enemyManager.AddComponent<EnemyScript>();
            enemyScript.ReadSpawnEnemyPrefab();

            CompletionBar completionBar = FindObjectOfType<CompletionBar>();
            completionBar.SetCompletionPercentage();

            inputHandler.SetActive(false);
            Time.timeScale = 0f;
            startLevelScreen.SetActive(true);
        }

        public void ExitStartScreen()
        {
            Time.timeScale = 1f;
            startLevelScreen.SetActive(false);
            inputHandler.SetActive(true);
            
        }
    }
}
