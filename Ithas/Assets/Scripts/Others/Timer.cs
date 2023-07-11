using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class Timer : MonoBehaviour
    {
        float currentTime = 0f;
        float startingTime = 60f;

        public GameObject levelFailPopUp;
        [SerializeField] TextMeshProUGUI countdownText;

        public GameObject inputHandler;

        private void Start()
        {
            currentTime = startingTime;
        }

        private void Update()
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = "Timer: " + currentTime.ToString("0");

            if (currentTime <= 10)
            {
                countdownText.color = Color.red;
            }

            if (currentTime <= 0)
            {
                currentTime = 0;
                levelFailPopUp.SetActive(true);
                inputHandler.SetActive(false);
            }
        }
    }
}

