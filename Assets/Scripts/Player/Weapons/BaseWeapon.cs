using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Player.Weapons
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        [SerializeField] private float _damage = 1f;
        public float Damage => _damage;
        public List<WeaponStats> WeaponStats => _weaponStats;
        public int CurrentLevel => _currentLevel;
        private DiContainer _container;
        private int _currentLevel = 1;
        private int _maxLevel = 8;

        private void Awake() => _container.Inject(this);
        protected virtual void Start() => SetStats(0);

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                if(enemyHealth == null) return;
                float damage = Random.Range(_damage / 2f, _damage * 1.5f);
                enemyHealth.TakeDamage(damage);
            }
        }

        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;
            SetStats(_currentLevel-1);
        }

        protected virtual void SetStats(int value) => _damage = _weaponStats[value].Damage;
        
        [Inject] private void Construct(DiContainer container)
        {
            _container = container;
        }
    }
}