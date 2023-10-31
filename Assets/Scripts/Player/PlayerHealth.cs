using System;
using System.Collections;
using GameCore.Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        public Action OnHealthChanged;
        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regeneration = 1f;

        private void Start() => StartCoroutine(Regeneration());

        public void Heal()
        {
            _currentHealth += 30;
            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke();
        }
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if(_currentHealth <=0)
                Debug.Log("playerDeath");
        }

        private IEnumerator Regeneration()
        {
            while (true)
            {
                if (_currentHealth < _maxHealth)
                    _currentHealth += _regeneration;
                OnHealthChanged?.Invoke();
                yield return _regenerationInterval;
            }
        }
    }
}