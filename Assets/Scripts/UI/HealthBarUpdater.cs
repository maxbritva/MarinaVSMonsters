using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthBarUpdater : MonoBehaviour
    {
        [SerializeField] private Image _imageHPbar;
        private PlayerHealth _playerHealth;

        private void OnEnable() => _playerHealth.OnHealthChanged += UpdateBar;

        private void OnDisable() => _playerHealth.OnHealthChanged -= UpdateBar;

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;

        private void UpdateBar()
        {
            _imageHPbar.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MAXHealth;
            _imageHPbar.fillAmount = Mathf.Clamp01(_imageHPbar.fillAmount);
        }
    }
}