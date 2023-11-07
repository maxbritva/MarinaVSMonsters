using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class FireBall : BaseWeapon
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _range;
        
        [Header("Single")] 
        [SerializeField] private SpriteRenderer _spriteRenderer1X;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Transform _targetSprite1X;
        [SerializeField] private Transform _targetContainer1X;
        [Header("Double")]
        [SerializeField] private List<SpriteRenderer> _spriteRenderer2X;
        [SerializeField] private List<Collider2D> _collider2X;
        [SerializeField] private List<Transform> _targetSprite2X;
        [SerializeField] private Transform _targetContainer2X;

        private WaitForSeconds _interval;
        private WaitForSeconds _duration;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _coroutine;
        private bool _isActivePhase;
        [SerializeField]private bool _isDoubleWeapon;

        protected override void Start()
        {
            Activate();
            SetStats(0);
            SetupRange();
        }

        private void Update() => transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        public override void LevelUp()
        {
            base.LevelUp();
            if(CurrentLevel < 4) return;
            _isDoubleWeapon = true;
            CheckForDoubleWeapon();
        }
        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel-1);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].Duration);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private void Activate()
        {
            _coroutine = StartCoroutine(LifeCycle());
            CheckForDoubleWeapon();
        }

        private void Deactivate() => StopCoroutine(_coroutine);

        private void CheckForDoubleWeapon()
        {
            if (_isDoubleWeapon == false)
            {
                _targetContainer1X.gameObject.SetActive(true);
                _targetContainer2X.gameObject.SetActive(false);
            }
            else
            {
                _targetContainer1X.gameObject.SetActive(false);
                _targetContainer2X.gameObject.SetActive(true);
                for (int i = 0; i < _collider2X.Count; i++) 
                    _collider2X[i].gameObject.SetActive(true);
            }
        }

        private IEnumerator LifeCycle()
        {
            while (true)
            {
                if (_isDoubleWeapon == false)
                {
                    _spriteRenderer1X.enabled = !_spriteRenderer1X.enabled;
                    _collider.enabled = !_collider.enabled;
                }
                else
                {
                    for (int i = 0; i < _spriteRenderer2X.Count; i++)
                        _spriteRenderer2X[i].enabled = !_spriteRenderer2X[i].enabled;
                    for (int i = 0; i < _collider2X.Count; i++) 
                        _collider2X[i].enabled = !_collider2X[i].enabled;
                }
                SetupPhase();
                yield return _interval;
            }
        }

        private void SetupPhase()
        {
            _isActivePhase = _isDoubleWeapon == false ? _spriteRenderer1X.enabled : _spriteRenderer2X[0].enabled;
            _interval = _isActivePhase ? _duration : _timeBetweenAttack;
        }

        private void SetupRange()
        {
            if (_isDoubleWeapon == false)
            {
                _targetSprite1X.transform.localPosition = new Vector3(_range,0,0);
                _collider.offset = new Vector3(_range,0,0);
            }
            else
            {
                _targetSprite2X[0].transform.localPosition = new Vector3(_range,0,0);
                _targetSprite2X[1].transform.localPosition = new Vector3(-_range,0,0);
                _collider2X[0].offset = new Vector3(_range,0,0);
                _collider2X[1].offset = new Vector3(-_range,0,0);
            }
        }
    }
}