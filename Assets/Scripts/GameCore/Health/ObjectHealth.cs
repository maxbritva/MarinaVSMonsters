using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _maxHealth;
        [SerializeField] protected float _currentHealth;

        public float MAXHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        private void OnEnable() => _currentHealth = _maxHealth;

        public virtual void TakeDamage(float damage)
        {
            if(damage <= 0)
                throw new  ArgumentOutOfRangeException(nameof(damage));
            _currentHealth -= damage;
        }
    }
}