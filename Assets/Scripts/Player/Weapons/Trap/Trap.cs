using System.Collections;
using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapons.Trap
{
    public class Trap : Throw
    {
        [SerializeField] private CircleCollider2D _collider2D;
        private WaitForSeconds _checkInterval = new WaitForSeconds(3f);
        private PlayerHealth _playerHealth;
        private TrapWeapon _trapWeapon;

        protected override void OnEnable()
        {
            Damage = _trapWeapon.Damage;
            _collider2D.enabled = false;
            StartCoroutine(CheckDistance());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if (enemy == null) return;
                enemy.TakeDamage(Damage);
                if(enemy.gameObject.activeSelf)
                    enemy.Burn(Damage);
                gameObject.SetActive(false);
            }
        }

        public void ActivateTrap() => _collider2D.enabled = true;

        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if(Vector3.Distance(transform.position, _playerHealth.transform.position) > 15f)
                    gameObject.SetActive(false);
                yield return _checkInterval;
            }
        }

        [Inject] private void Construct(PlayerHealth health, TrapWeapon weapon)
        {
            _playerHealth = health;
            _trapWeapon = weapon;
        }
    }
}