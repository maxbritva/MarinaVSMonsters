using System;
using UnityEngine;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSystem : MonoBehaviour
    {
        public Action<int> OnExperiencePickedUp;
        [SerializeField] private GameObject _upgradeWindow;
        private int _currentExperience, _experienceToLevelUp = 5, _currentLevel = 1;
        public int CurrentExperience => _currentExperience;
        public int ExperienceToLevelUp => _experienceToLevelUp;
        public int CurrentLevel => _currentLevel;

        private void OnEnable() => OnExperiencePickedUp += ExperienceAddValue;

        private void OnDisable() => OnExperiencePickedUp -= ExperienceAddValue;

        private void ExperienceAddValue(int value)
        {
            if(value <=0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _currentExperience += value;
            if (_currentExperience >= _experienceToLevelUp)
                LevelUp();
        }

        private void LevelUp()
        {
            _currentExperience = 0;
            _currentLevel++;
           // _upgradeWindow.SetActive(true);
           switch (_currentLevel)
           {
               case <= 20:
                   _experienceToLevelUp += 10;
                   break;
               case <= 40:
                   _experienceToLevelUp += 13;
                   break;
               case <= 60:
                   _experienceToLevelUp += 16;
                   break;
           }
        }
    }
}