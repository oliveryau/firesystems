//celine
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class AchievementItemController : MonoBehaviour // for single achievement item
    {
        public string id;
        private static Dictionary<string, bool> achievementUnlockedStatus = new Dictionary<string, bool>();
        private bool _isUnlocked = false;

        [SerializeField] TextMeshProUGUI achievementId;
        [SerializeField] TextMeshProUGUI achievementNameTexts;
        [SerializeField] TextMeshProUGUI achievementDescriptionTexts;
        [SerializeField] Image greyOut;
        [SerializeField] Image tick;

        public Achievement achievement;

        public bool isUnlocked
        {
            get { return _isUnlocked; }
            set
            {
                if (_isUnlocked != value)
                {
                    _isUnlocked = value;
                    achievementUnlockedStatus[id] = value;
                    UpdateUi();
                }
            }
        }

        private void Start()
        {
            LoadUnlockedStatus();
            UpdateUi();
        }

        public void ShowAchievements()
        {
            achievementId.text = achievement.achievementId.ToString();
            achievementNameTexts.text = achievement.achievementName;
            achievementDescriptionTexts.text = achievement.achievementDescription;
            UpdateUi();
        }

        private void UpdateUi()
        {
            achievement.achievementIsUnlocked = _isUnlocked;
            greyOut.enabled = !_isUnlocked;
            tick.enabled = _isUnlocked;
        }

        private void LoadUnlockedStatus()
        {
            if (achievementUnlockedStatus.TryGetValue(id, out bool unlockedValue))
            {
                _isUnlocked = unlockedValue;
            }
            else
            {
                _isUnlocked = false;
                achievementUnlockedStatus[id] = false;
            }
        }

        public static void ResetAllUnlockedStatus()
        {
            foreach (var key in achievementUnlockedStatus.Keys.ToList())
            {
                achievementUnlockedStatus[key] = false;
            }
        }
    }
}
