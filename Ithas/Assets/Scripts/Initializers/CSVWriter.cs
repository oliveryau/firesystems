using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Ithas
{
    public class CSVWriter : MonoBehaviour
    {
        string filename = "";

        public AnalyticsController analyticsController;

        [System.Serializable]
        public class Analytics
        {
            public int levelId;
            public int startPlayerLevel;
            public int endPlayerLevel;
            public string completionRate;
            public string timeTaken;
            public float damageTaken;
            public float totalExpGained;
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

        public void WriteCsv()
        {
            analyticsController.DoAnalytics();

            var newAnalytics = new Analytics
            {
                levelId = analyticsController.levelId,
                startPlayerLevel = analyticsController.startPlayerLevel,
                endPlayerLevel = analyticsController.endPlayerLevel,
                completionRate = analyticsController.completionRate.ToString("0.00"),
                timeTaken = analyticsController.timeTaken.ToString("0.00"),
                damageTaken = analyticsController.damageTaken,
                totalExpGained = analyticsController.totalExpGained,
                enemiesKilled = analyticsController.enemiesKilled
            };

            List<Analytics> existingAnalyticsList = new List<Analytics>(analyticsList.analytics);
            existingAnalyticsList.Add(newAnalytics);
            analyticsList.analytics = existingAnalyticsList.ToArray();

            bool fileExists = File.Exists(filename);

            using (TextWriter tw = new StreamWriter(filename, true))
            {
                if (!fileExists)
                {
                    tw.WriteLine("levelId, startPlayerLevel, endPlayerLevel, completionRate, timeTaken, damageTaken, totalExpGained, enemiesKilled");
                }

                foreach (var data in analyticsList.analytics) // Append the data to the file
                {
                    tw.WriteLine(data.levelId + "," + data.startPlayerLevel + "," +
                                 data.endPlayerLevel + "," + data.completionRate + "," +
                                 data.timeTaken + "," + data.damageTaken + "," +
                                 data.totalExpGained + "," + data.enemiesKilled);

                    Debug.Log(data.levelId + "," + data.startPlayerLevel + "," +
                              data.endPlayerLevel + "," + data.completionRate + "," +
                              data.timeTaken + "," + data.damageTaken + "," +
                              data.totalExpGained + "," + data.enemiesKilled);
                }
            }
        }
    }
}