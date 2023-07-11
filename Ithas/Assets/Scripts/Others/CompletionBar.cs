using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class CompletionBar : MonoBehaviour
    {
        public Slider completionBar;
        public int totalObjects; // Total number of specific static game objects in the level
        public int destroyedObjects; // Number of destroyed game objects
        public float completionPercentage; // Completion percentage
        public GameObject[] burnableObjects;
        public GameObject levelCompletionPopUp;
        [SerializeField] TextMeshProUGUI completionText;

        public GameObject inputHandler;

        public void SetCompletionPercentage() // Only for starting
        {
            burnableObjects = GameObject.FindGameObjectsWithTag("Burnables");
            totalObjects = burnableObjects.Length;
            completionPercentage = (float)destroyedObjects / totalObjects * 100f;
            SetCompletionBar();
        }

        public void UpdateCompletionPercentage() // Actual value
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

            // Check if all game objects have been destroyed (100% completion)
            if (completionPercentage >= 100)
            {
                levelCompletionPopUp.SetActive(true);
                inputHandler.SetActive(false);
            }
        }
    }

}