//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class PauseMenu : MonoBehaviour, InputReceiver
    {
        private bool isPaused;

        public InputHandler inputHandler;
        public GameObject pauseScreen;
        public GameObject achievementScreen;
        public string sceneToLoad;

        private void Start()
        {
            isPaused = false;
        }

        public void PauseGame()
        {
            pauseScreen.SetActive(true);
            achievementScreen.SetActive(false);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void ResumeGame()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            inputHandler.SetInputReceiver(inputHandler.playerMovement);
        }

        public void ExitMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }

        public void ShowAchievements()
        {
            achievementScreen.SetActive(true);
        }

        #region Input Handling

        public void DoMove(Vector2 moving)
        {
            //do nothing
        }

        public void DoAttack()
        {
            //do nothing
        }

        public void DoPause()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        #endregion
    }
}
