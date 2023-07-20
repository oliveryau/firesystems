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
        public float startingTime = 60f;
        private bool hasWrittenData;

        public CSVWriter csvWriter;
        public GameObject levelFailPopUp;
        [SerializeField] TextMeshProUGUI countdownText;

        public GameObject inputHandler;

        private void Start()
        {
            currentTime = startingTime;
            hasWrittenData = false;

    }

    private void Update()
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = "Timer: " + currentTime.ToString("0");

            if (currentTime <= 10f)
            {
                countdownText.color = Color.red;
            }

            if (currentTime <= 0f && !hasWrittenData)
            {
                currentTime = 0f;
                levelFailPopUp.SetActive(true);
                inputHandler.SetActive(false); //player input disabled when UI is shown
                csvWriter.WriteCsv();
                hasWrittenData = true;
            }
        }
    }
}

