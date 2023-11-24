using System.Collections;
using GameCore.Pool;
using UnityEngine;

namespace Player.Weapons.Trap
{
    public class TrapWeapon : BaseWeapon
    {
        [SerializeField] private ObjectPool _trapPool;
        [SerializeField] private Transform _container;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _coroutine;

        private void OnEnable() => Activate();

        private void Activate()
        {
            SetStats(0);
            _coroutine = StartCoroutine(StartSpawn());
        }

        private void Deactivate() => StopCoroutine(_coroutine);

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                GameObject trapGromPool = _trapPool.GetFromPool();
                trapGromPool.transform.SetParent(_container);
                trapGromPool.transform.position = transform.position;
                yield return _timeBetweenAttack;
            }
        }
    }
}