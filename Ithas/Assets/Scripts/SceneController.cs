using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ithas
{
    public class SceneController : MonoBehaviour
    {
        public string SceneToLoad;
        public PlayerStatsSO playerStatsSO;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                GameSaveManager.Instance.SaveData(playerStatsSO);

                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }
}
