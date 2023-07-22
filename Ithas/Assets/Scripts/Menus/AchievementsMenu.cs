using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.VersionControl;

namespace Ithas
{
    public class AchievementsMenu : MonoBehaviour
    {
        public Achievement[] achievements; // all achievements
        public GameObject achievementItemPrefab;
        public Transform content;
        public string achievementType;

        public GameController gameController;
        public AchievementItemController achievementItemController;
        public List<AchievementItemController> achievementItemControllers = new List<AchievementItemController>();

        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
            achievements = gameController.GetAchievement();

            for (int i = 0; i < achievements.Length; i++)
            {
                GameObject obj = Instantiate(achievementItemPrefab, content);   //spawn each achievement
                achievementItemController = obj.GetComponent<AchievementItemController>();
                achievementItemController.achievement = achievements[i];
                achievementItemController.ShowAchievements();
                achievementItemControllers.Add(achievementItemController); // Add each controller to the list
                Debug.Log(achievements.Length);
            }
        }

        private void Update()
        {
            //UpdateAchievements(achievements);
        }

        public void UpdateAchievements(Achievement[] achievement)
        {
            Debug.Log(achievement.Length);

            if (achievement != null && achievement.Length > 0)
            {
                for (int i = 0; i < achievement.Length; i++)
                {
                    switch (achievementItemControllers[i].achievement.achievementType)
                    {
                        case "Time":
                            {
                                Debug.Log(achievementItemControllers[i].achievement.achievementType);
                                break;
                            }
                        case "Kill":
                            {
                                Debug.Log(achievementItemControllers[i].achievement.achievementType);
                                if (gameController.enemiesKilled >= 1)
                                {
                                    achievementItemControllers[i].achievement.achievementIsUnlocked = true;
                                }
                                break;
                            }
                        case "Destroy":
                            {
                                Debug.Log(achievementItemControllers[i].achievement.achievementType);
                                break;

                            }
                        case "Level":
                            {
                                Debug.Log(achievementItemControllers[i].achievement.achievementType);
                                break;
                            }
                        case "Player":
                            {
                                Debug.Log(achievementItemControllers[i].achievement.achievementType);
                                if (gameController.currentPlayerLevel >= achievements[i].achievementValue)
                                {
                                    achievementItemControllers[i].isUnlocked = true;
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                Debug.Log("No achievements found!");
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
