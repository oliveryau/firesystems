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
                    playerStatsSO.ResetToInitialStats();
                }
                else
                {
                    GameSaveManager.Instance.SaveData(playerStatsSO); //save data into SO
                }
                StartCoroutine(FadeCoroutine());
            }
        }

        public void RetryLevel() //when fail and retry button
        {
            StartCoroutine(RetryFadeCoroutine());
        }

        public void BackToOutdoor()
        {
            StartCoroutine(FadeCoroutine());
        }

        public IEnumerator RetryFadeCoroutine() //when fail and retry button
        {
            playerStatsSO.ResetToInitialStats();
            inputHandler.SetActive(true);
            if (fadeOutPanel != null)
            {
                Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            }
            yield return new WaitForSeconds(fadeDelay);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentScene);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }

        public IEnumerator FadeCoroutine()
        {
            inputHandler.SetActive(true);
            Time.timeScale = 1f;
            if (fadeOutPanel != null)
            {
                Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            }
            yield return new WaitForSeconds(fadeDelay);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}