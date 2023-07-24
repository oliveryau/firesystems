//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class AchievementsMenu : MonoBehaviour
    {
        public Achievement[] achievements; // all achievements
        public GameObject achievementItemPrefab;
        public Transform content;
        public string achievementType;

        public AchievementItemController achievementItemController;
        public List<AchievementItemController> achievementItemControllers = new List<AchievementItemController>();

        [Header("Others")]
        public GameController gameController;

        public void InitializeMenu()
        {
            gameController = FindObjectOfType<GameController>();

            achievements = gameController.GetAchievement();

            for (int i = 0; i < achievements.Length; i++)
            {
                GameObject obj = Instantiate(achievementItemPrefab, content);   //spawn each achievement
                achievementItemController = obj.GetComponent<AchievementItemController>();
                achievementItemController.achievement = achievements[i];
                achievementItemController.id = "Achievement_" + achievements[i].achievementId; // Set the id field based on achievementId
                achievementItemController.ShowAchievements();
                achievementItemControllers.Add(achievementItemController); // Add each controller to the list
            }
        }

        public void UpdateAchievements(string achievementType)
        {
            if (achievements != null && achievements.Length > 0)
            {
                for (int i = 0; i < achievements.Length; i++)
                {
                    if (achievements[i].achievementType == achievementType)
                    {
                        switch (achievementType)
                        {
                            case "Kill":
                                {
                                    if (gameController.enemiesKilled >= achievements[i].achievementValue)
                                    {
                                        achievementItemControllers[i].isUnlocked = true;
                                    }
                                    break;
                                }
                            case "Destroy":
                                {
                                    if (gameController.objectsDestroyed >= achievements[i].achievementValue)
                                    {
                                        achievementItemControllers[i].isUnlocked = true;
                                    }
                                    break;
                                }
                            case "Player":
                                {
                                    if (gameController.currentPlayerLevel >= achievements[i].achievementValue)
                                    {
                                        achievementItemControllers[i].isUnlocked = true;
                                    }
                                    break;
                                }
                            default:
                                {
                                    Debug.Log("No achievementType found");
                                    break;
                                }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No achievementType found!");
            }
        }


    }

    [System.Serializable]
    public class Achievement
    {
        public int achievementId;
        public string achievementName;
        public string achievementType;
        public int achievementValue;
        public string achievementDescription;
        public bool achievementIsUnlocked;
    }
}
