using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ithas
{
    public class StatsMenu : MonoBehaviour
    {
        public GameObject statsScreen;
        public GameObject gameOverScreen;
        public GameObject levelCompleteScreen;
        public AnalyticsController analyticsController;
        public CompletionBar completionBar;
        public TextMeshProUGUI levelId;
        public TextMeshProUGUI startPlayerLevel;
        public TextMeshProUGUI endPlayerLevel;
        public TextMeshProUGUI completionRate;
        public TextMeshProUGUI timeTaken;
        public TextMeshProUGUI damageTaken;
        public TextMeshProUGUI totalExpGained;
        public TextMeshProUGUI enemiesKilled;

        public void ShowStats()
        {
            levelId.text = "Level: " + analyticsController.levelId.ToString();
            startPlayerLevel.text = "Initial Player Level: " + analyticsController.startPlayerLevel.ToString();
            endPlayerLevel.text = "End Player Level: " + analyticsController.endPlayerLevel.ToString();
            completionRate.text = "Completion Rate: " + analyticsController.completionRate.ToString("0.00");
            timeTaken.text = "Time Taken: " + analyticsController.timeTaken.ToString("0.00");
            damageTaken.text = "Damage Taken: " + analyticsController.damageTaken.ToString();
            totalExpGained.text = "Total EXP Gained: " + analyticsController.totalExpGained.ToString();
            enemiesKilled.text = "Enemies Killed: " + analyticsController.enemiesKilled.ToString();

            gameOverScreen.SetActive(false);
            levelCompleteScreen.SetActive(false);
            statsScreen.SetActive(true);
        }
        public void CloseStats()
        {
            statsScreen.SetActive(false);
            if (completionBar.completionPercentage < 100) //for gameover
            {
                gameOverScreen.SetActive(true);
            }
            else //for level complete
            {
                levelCompleteScreen.SetActive(true);
            }
        }
    }
}
