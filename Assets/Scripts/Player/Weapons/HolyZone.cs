using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Player.Weapons
{
    public class HolyZone : BaseWeapon
    {
        [SerializeField] private float _range;
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider2D;
        private List<EnemyHealth> _enemyInZone = new List<EnemyHealth>();
        private Coroutine _coroutine;
        private WaitForSeconds _timeBetweenAttack;

        private void OnEnable() => Activate();

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if(enemy == null) return;
                _enemyInZone.Add(enemy);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy)) 
                _enemyInZone.Remove(enemy);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel -1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel-1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _collider2D.radius = _range / 3f;
        }

        private void Activate()
        {
            SetStats(0);
            _coroutine = StartCoroutine(CheckZone());
        }

        private void Deactivate() => StopCoroutine(_coroutine);

        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = 0; i < _enemyInZone.Count; i++) 
                    _enemyInZone[i].TakeDamage(Damage);
                yield return _timeBetweenAttack;
            }
        }
    }
}