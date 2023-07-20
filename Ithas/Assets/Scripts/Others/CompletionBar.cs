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
        [Header("Values")]
        private int totalObjects; // total number of specific static game objects in the level
        private int destroyedObjects; // number of destroyed game objects
        [HideInInspector] public float completionPercentage; // completion percentage
        private GameObject[] burnableObjects; // to store all burnable objects

        [Header("UI")]
        public Slider completionBar;
        public GameObject levelCompletionPopUp; // finish Screen
        [SerializeField] TextMeshProUGUI completionText; // finish Text

        [Header("Others")]
        public GameObject inputHandler;
        public Timer timer;
        public CSVWriter csvWriter;

        public void SetCompletionPercentage() // only for starting
        {
            burnableObjects = GameObject.FindGameObjectsWithTag("Burnables");
            totalObjects = burnableObjects.Length; // Set total amount of burnable objects
            UpdateCompletionPercentage();
        }

        public void UpdateCompletionPercentage() // actual value
        {
            completionPercentage = (float)destroyedObjects / totalObjects * 100f;
            SetCompletionBar();
        }

        public void SetCompletionBar() // UI
        {
            completionBar.value = completionPercentage/100;
            completionText.text = "Completion Rate: " + completionPercentage.ToString("0.00") + "%";
        }

        public void ObjectDestroyed()
        {
            destroyedObjects++;
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