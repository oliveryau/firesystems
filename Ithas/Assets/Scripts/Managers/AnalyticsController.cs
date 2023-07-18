using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class AnalyticsController : MonoBehaviour
    {
        public StartLevel startLevel;
        public PlayerStats playerStats;
        public PlayerStatsSO playerStatsSO;
        public CompletionBar completionBar;
        public Timer timer;
        public GameController gameController;

        public int levelId;
        public int startPlayerLevel;
        public int endPlayerLevel;
        public float completionRate;
        public float timeTaken;
        public float damageTaken;
        public float totalExpGained;
        public int enemiesKilled;


        public void DoAnalytics()
        {
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