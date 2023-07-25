//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class SceneController : MonoBehaviour
    {
        public string currentScene;
        public string sceneToLoad;

        public float fadeDelay;
        public GameObject fadeInPanel;
        public GameObject fadeOutPanel;

        [Header("SO")]
        public PlayerStatsSO playerStatsSO;

        [Header("Others")]
        public GameObject inputHandler;
        public GameObject runOutCanvas;


        private void Awake()
        {
            if (fadeInPanel != null)
            {
                GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
                Destroy(panel, 1f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                if (currentScene == "Level") //currentScene == "Level 2"
                {
                    runOutCanvas.SetActive(true);
                    Time.timeScale = 0f;
                    inputHandler.SetActive(false);
                }
                else
                {
                    GameSaveManager.Instance.SaveData(playerStatsSO); //save data into SO
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }

        public void RetryLevel() //when fail and retry button
        {
            Time.timeScale = 1f;
            playerStatsSO.ResetToInitialStats();
            SceneManager.LoadScene(currentScene);
        }

        public void BackToOutdoor() //when complete level and exit/fail level and exit
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }

        public void BackToOutdoorLoseStats() //when manually exit level
        {
            Time.timeScale = 1f;
            playerStatsSO.ResetToInitialStats();
            SceneManager.LoadScene(sceneToLoad);
        }

        public void StayInLevel() //stay in level
        {
            Time.timeScale = 1f;
            inputHandler.SetActive(true);
            runOutCanvas.SetActive(false);
        }
    }
}