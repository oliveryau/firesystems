using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Ithas
{
    public class CSVWriter : MonoBehaviour
    {
        string filename = "";

        public GameController gameController;

        [System.Serializable]
        public class Analytics
        {
            public int levelId;
            public int startPlayerLevel;
            public int endPlayerLevel;
            public float completionRate;
            public float timeTaken;
            public float damageTaken;
            public int totalExpGained;
            public int enemiesKilled;
        }

        [System.Serializable]
        public class AnalyticsList
        {
            public Analytics[] analytics;
        }

        public AnalyticsList analyticsList = new AnalyticsList();

        private void Start()
        {
            filename = Application.dataPath + "/Analytics.csv";
        }

        private void Update(){

        }

        // 1) call this method when press ok after completing level, in CompletionBar.ObjectsDestroyed() within the loop when >= 100%
        // 2) call this method when failed level, in Timer script's Update method when currentTime <= 0f
        // 3) call this method when player die, in gameController.PlayerDie();
        public void WriteCsv(){
            if (analyticsList.analytics.Length > 0)
            {
                TextWriter tw = new StreamWriter(filename, false);
                tw.WriteLine("levelId, startPlayerLevel, endPlayerLevel, completionRate, timeTaken, damageTaken, totalExpGained, enemiesKilled");
                tw.Close();

                tw = new StreamWriter(filename, true);

                for (int i = 0; i < analyticsList.analytics.Length; i++)
                {
                    //go and do whatever is below first then do this at the end

                    //set analyticsList.analytics[i].levelId = analyticsController.levelId
                    //set analyticsList.analytics[i].startPlayerLevel = analyticsController.startPlayerLevel
                    //set analyticsList.analytics[i].endPlayerLevel = analyticsController.endPlayerLevel
                    //etc etc.

                    tw.WriteLine(analyticsList.analytics[i].levelId + "," + analyticsList.analytics[i].startPlayerLevel + "," +
                                 analyticsList.analytics[i].endPlayerLevel + "," + analyticsList.analytics[i].completionRate + "," +
                                 analyticsList.analytics[i].timeTaken + "," + analyticsList.analytics[i].damageTaken + "," + 
                                 analyticsList.analytics[i].totalExpGained + "," + analyticsList.analytics[i].enemiesKilled);
                }

                tw.Close();
            }
        }

        //create an empty gameobject in unity then drag it to the Level scene

        //whatever is below should be set in the analyticsControllerScript
        //declare every variable similar to the Analytics system.serializable class

        //set levelId = startLevel.levelId

        //set startPlayerLevel = playerStatsSO.initialLevel (I think you can declare public variable for playerStatsSO)

        //set endPlayerLevel = playerStatsSO.level

        //set completionRate = completionBar.completionPercentage

        //set timeTaken = timer.startingTime - timer.currentTime

        //set damageTaken = playerStats.totalDamageTaken

        //firstly u needa make a public float variable in my PlayerStats script called totalDamageTaken (or whatever u want) (for ocd sake u can hideininspector unless u wanna see if it works first)
        //actually can be int cus the damage got no decimals but i set the damage as float elsewhere so i lazy change back, so just put float
        //then in the DamagePlayer() in gameController, add the line within the method "playerStats.totalDamageTaken += damage;" anywhere after line 108 (assuming u named it totalDamageTaken)
        //so damageTaken = playerStats.totalDamageTaken (basically this keeps adding the total damage taken)

        //set totalExpGained = playerStats.totalExpGained;

        //same thing make a public int variable in my PlayerStats script called totalExpGained (or whatever u want, this time int cus i think i set it as int)
        //then in EnemyDie() in gameController, add the line "playerStats.totalExpGained += playerStats.currentExp;" anywhere after line 146
        //so totalExpGained = playerStats.totalExpGained

        //set enemiesKilled = gameController.enemiesKilled
        //same same but different, instead declare the public int variable within gameController script called enemiesKilled (or whatever u want)
        //then in EnemyDie() in gameController, just increment the enemiesKilled
        //so enemiesKilled = gameController.enemiesKilled
    }
}