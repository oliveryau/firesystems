using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class SceneController : MonoBehaviour
    {
        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name.Equals("Main"))
            {
                EnemyScript enemyScript = FindObjectOfType<EnemyScript>();
                if (enemyScript != null)
                {
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController != null)
                    {
                        enemyScript.Initialize(gameController); //enemyScript initialize method
                    }
                }
            }
        }
    }
}
