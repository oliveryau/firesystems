//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class GameOverMenu : MonoBehaviour
    {
        public GameObject gameOverScreen;
        public string sceneToLoad;

        public void GameOver()
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        public void RetryGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
