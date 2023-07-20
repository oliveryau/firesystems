//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ithas
{
    public class Timer : MonoBehaviour
    {
        [HideInInspector] public float currentTime = 0f;
        [Header("Timer")]
        public float startingTime = 60f;
        private bool hasWrittenData;
        [SerializeField] TextMeshProUGUI countdownText;

        [Header("Others")]
        public CSVWriter csvWriter;
        public GameObject levelFailPopUp;
        public GameObject inputHandler;

        private void Start()
        {
            currentTime = startingTime;
            hasWrittenData = false;
        }

        private void Update()
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = "Timer: " + currentTime.ToString("0.00"); // display time

            if (currentTime <= 10f)
            {
                countdownText.color = Color.red; // display time in red if <10s
            }

            if (currentTime <= 0f && !hasWrittenData)
            {
                csvWriter.WriteCsv();
                currentTime = 0f;
                Time.timeScale = 0f;
                levelFailPopUp.SetActive(true);
                inputHandler.SetActive(false); // player input disabled when UI is shown
                hasWrittenData = true;
            }
        }
    }
}

