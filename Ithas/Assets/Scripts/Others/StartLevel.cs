using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class StartLevel : MonoBehaviour
    {
        public GameObject startLevelScreen;
        public GameObject inputHandler;

        private void Start()
        {
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
