//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class CompletionBar : MonoBehaviour
    {
        private GameController gameController;

        [Header("Values")]
        private int totalObjects; // total number of specific static game objects in the level
        private GameObject[] burnableObjects; // to store all burnable objects
        public float completionPercentage; // completion percentage

        [Header("UI")]
        public Slider completionBar;
        public GameObject levelCompletionPopUp; // finish Screen
        [SerializeField] TextMeshProUGUI completionText; // finish Text

        [Header("Others")]
        public GameObject inputHandler;
        public Timer timer;
        public CSVWriter csvWriter;

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
        }

        public void SetCompletionPercentage() // only for starting
        {
            burnableObjects = GameObject.FindGameObjectsWithTag("Burnables");
            totalObjects = burnableObjects.Length; // Set total amount of burnable objects
            UpdateCompletionPercentage();
        }

        public void UpdateCompletionPercentage() // actual value
        {
            completionPercentage = (float)gameController.objectsDestroyed / totalObjects * 100f;
            SetCompletionBar();
        }

        public void SetCompletionBar() // UI
        {
            completionBar.value = completionPercentage/100;
            completionText.text = "Completion Rate: " + completionPercentage.ToString("0.00") + "%";
        }

        public void ObjectDestroyed()
        {
            UpdateCompletionPercentage();

            if (completionPercentage >= 100) // check if all game objects have been destroyed
            {
                csvWriter.WriteCsv();
                Time.timeScale = 0f;
                levelCompletionPopUp.SetActive(true);
                inputHandler.SetActive(false); // player input disabled when UI is shown
            }
        }
    }

}