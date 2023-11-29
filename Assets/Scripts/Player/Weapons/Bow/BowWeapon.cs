using System.Collections;
using GameCore.Pool;
using UnityEngine;
using Zenject;

namespace Player.Weapons.Bow
{
    public class BowWeapon : BaseWeapon
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private ObjectPool _arrowPool;
        [SerializeField] private Animator _animator;
        private WaitForSeconds _timeBetweenAttack;
        private PlayerController _playerController;
        private Coroutine _coroutine;
        private float _duration, _speed;
        public float Duration => _duration;
        public float Speed => _speed;

        private void Update()
        {
            _weaponTransform.transform.rotation = Quaternion.Lerp(_weaponTransform.rotation, Quaternion.Euler(0,0,
                (Mathf.Atan2(_playerController.Movement.y, _playerController.Movement.x)) 
                * Mathf.Rad2Deg + 90f), 8f * Time.deltaTime);
            _spriteRenderer.enabled = _playerController.Movement != Vector3.zero;
        }

        private void OnEnable() => Activate();

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel-1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel-1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }

        private void Activate()
        {
            SetStats(0);
            _coroutine = StartCoroutine(StartThrow());
        }

        private void Deactivate() => StopCoroutine(_coroutine);

        private IEnumerator StartThrow()
        {
            while (true)
            {
                if (_playerController.Movement != Vector3.zero)
                {
                    _spriteRenderer.enabled = true;
                    _animator.SetTrigger("Attack");
                }
                yield return _timeBetweenAttack;
            }
        }

        public void ThrowArrow()
        {
            GameObject newArrow = _arrowPool.GetFromPool();
            newArrow.transform.SetParent(_container);
            newArrow.transform.position = _shootPoint.position;
            newArrow.transform.rotation = transform.rotation;
            _animator.SetTrigger("Idle");
        }

        [Inject] private void Construct(PlayerController controller) => _playerController = controller;
    }
}