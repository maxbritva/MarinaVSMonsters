using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        [SerializeField] private Image _playerHealhImage;
        private PlayerHealth _playerHealth;

        private void OnEnable() => _playerHealth.OnHealthChanged += UpdateHealthBar;
        private void OnDisable() => _playerHealth.OnHealthChanged -= UpdateHealthBar;

        [Inject] private void Construct(PlayerHealth playerHealth) => _playerHealth = playerHealth;

        private void UpdateHealthBar()
        {
            _playerHealhImage.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MAXHealth;
            _playerHealhImage.fillAmount = Mathf.Clamp01(_playerHealhImage.fillAmount);
        }
    }
}