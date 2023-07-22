using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine.SocialPlatforms.Impl;

namespace Ithas
{
    public class AchievementsMenu : MonoBehaviour
    {
        private Achievement[] achievements; // all achievements
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
                GameObject obj = Instantiate(achievementItemPrefab, content);
                achievementItemController = obj.GetComponent<AchievementItemController>();
                achievementItemController.achievement = achievements[i];
                achievementItemController.ShowAchievements();
            }
        }

        private void Update()
        {
            UpdateAchievements(achievements);
        }

        public void UpdateAchievements(Achievement[] achievement)
        {
            if (achievements != null && achievements.Length > 0)
            {
                for (int i = 0; i < achievement.Length; i++)
                {
                    switch (achievementType)
                    {
                        case "Time":
                            {
                                return;
                            }
                        case "Kill":
                            {
                                if (gameController.enemiesKilled >= achievement[i].achievementValue)
                                {
                                    achievementItemController.isUnlocked = true;
                                }
                                break;
                            }
                        case "Destroy":
                            {
                                break;

                            }
                        case "Level":
                            {
                                break;

                            }
                        case "Player":
                            {
                                if (gameController.currentPlayerLevel >= achievement[i].achievementValue)
                                {
                                    achievementItemController.isUnlocked = true;
                                }
                                break;
                            }
                    }
                    Debug.Log(achievementItemController.isUnlocked == true);
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
    }
}
