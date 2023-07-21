//celine
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class AnalyticsController : MonoBehaviour
    {
        [HideInInspector] public string currentDateTime;
        [HideInInspector] public int levelId;
        [HideInInspector] public int startPlayerLevel;
        [HideInInspector] public int endPlayerLevel;
        [HideInInspector] public float completionRate;
        [HideInInspector] public float timeTaken;
        [HideInInspector] public float damageTaken;
        [HideInInspector] public float totalExpGained;
        [HideInInspector] public int enemiesKilled;

        [Header("Others")]
        public StartLevel startLevel;
        public PlayerStats playerStats;
        public PlayerStatsSO playerStatsSO;
        public CompletionBar completionBar;
        public Timer timer;
        public GameController gameController;

        public void DoAnalytics()
        {
            currentDateTime = DateTime.Now.ToString();
            levelId = startLevel.levelId;
            startPlayerLevel = playerStatsSO.initialLevel;
            endPlayerLevel = playerStatsSO.level;
            completionRate = completionBar.completionPercentage;
            timeTaken = timer.startingTime - timer.currentTime;
            damageTaken = playerStats.totalDamageTaken;
            totalExpGained = playerStats.totalExpGained;
            enemiesKilled = gameController.enemiesKilled;
        }

    }
}