using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class AchievementItemController : MonoBehaviour // for single achievement item
    {
        [SerializeField] TextMeshProUGUI achievementId;
        [SerializeField] TextMeshProUGUI achievementNameTexts;
        [SerializeField] TextMeshProUGUI achievementDescriptionTexts;
        [SerializeField] Image greyOut;
        [SerializeField] Image tick;
        public bool isUnlocked;

        public Achievement achievement;

        public void ShowAchievements()
        {
            achievementId.text = achievement.achievementId.ToString();
            achievementNameTexts.text = achievement.achievementName;
            achievementDescriptionTexts.text = achievement.achievementDescription;

            greyOut.enabled = !isUnlocked;
            tick.enabled = isUnlocked;
        }

        private void OnValidate()
        {
            ShowAchievements();
        }
    }

}
