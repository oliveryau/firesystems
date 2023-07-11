using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ithas
{
    public class Timer : MonoBehaviour
    {
        private float currentTime = 0f;
        private float startingTime = 60f;

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

            if (currentTime <= 10f)
            {
                countdownText.color = Color.red;
            }

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                levelFailPopUp.SetActive(true);
                inputHandler.SetActive(false); //player input disabled when UI is shown
            }
        }
    }
}

