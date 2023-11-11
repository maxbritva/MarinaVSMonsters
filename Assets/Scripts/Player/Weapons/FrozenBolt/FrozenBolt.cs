using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapons.FrozenBolt
{
    public class FrozenBolt : Throw
    {
        private FrozenBoltWeapon _weapon;
        private void Update() => transform.position += transform.up * (5 * Time.deltaTime);

        protected override void OnEnable()
        {
            base.OnEnable();
             Timer = new WaitForSeconds(_weapon.Duration);
             Damage = _weapon.Damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if(enemy == null) return;
                enemy.TakeDamage(Damage);
                enemy.GetComponent<EnemyMove>().Freeze();
            }
            if(_weapon.CurrentLevel <= 4)
                gameObject.SetActive(false);
        }

        [Inject] private void Construct(FrozenBoltWeapon weapon) => _weapon = weapon;
    }
}