using System;
using GameCore.ExperienceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ExperienceUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Image _fillImage;
        private ExperienceSystem _experienceSystem;

        private void Start()
        {
            _fillImage.fillAmount = 0f;
            _expText.text = "1 LVL";
        }

        private void OnEnable() => _experienceSystem.OnExperiencePickedUp += UpdateExperienceBar;

        private void OnDisable() => _experienceSystem.OnExperiencePickedUp -= UpdateExperienceBar;

        private void UpdateExperienceBar(int exp)
        {
            _fillImage.fillAmount = (float)_experienceSystem.CurrentExperience / _experienceSystem.ExperienceToLevelUp;
            _fillImage.fillAmount = Mathf.Clamp01(_fillImage.fillAmount);
            _expText.text = $"{_experienceSystem.CurrentLevel} LVL";
        }

        [Inject] private void Construct(ExperienceSystem experienceSystem) => _experienceSystem = experienceSystem;
    }
}