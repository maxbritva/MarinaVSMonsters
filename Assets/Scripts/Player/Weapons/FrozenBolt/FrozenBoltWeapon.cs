using System.Collections;
using System.Collections.Generic;
using GameCore.Pool;
using UnityEngine;

namespace Player.Weapons.FrozenBolt
{
    public class FrozenBoltWeapon : BaseWeapon
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _coroutine;
        private float _duration, _speed;
        private Vector3 _direction;
        public float Duration => _duration;
        public float Speed => _speed;

        private void OnEnable() => Activate();

        private void Activate()
        {
            SetStats(0);
            _coroutine = StartCoroutine(StartThrow());
        }

        private void Deactivate() => StopCoroutine(_coroutine);

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel -1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel -1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }

        private IEnumerator StartThrow()
        {
            while (true)
            {
                for (int i = 0; i < _shootPoints.Count; i++)
                {
                    _direction = (_shootPoints[i].position - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject newFrozenBolt = _pool.GetFromPool();
                    newFrozenBolt.transform.SetParent(_container);
                    newFrozenBolt.transform.position = transform.position;
                    newFrozenBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                yield return _timeBetweenAttack;
            }
        }
    }
}