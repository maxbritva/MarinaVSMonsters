
using System;
using UnityEngine;
using Zenject;

namespace Player.Weapons.Suriken
{
    public class Suriken : Throw
    {
        [SerializeField] private Transform _sprite;
        private SurikenWeapon _surikenWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_surikenWeapon.Duration);
            Damage = _surikenWeapon.Damage;
        }

        private void Update()
        {
            _sprite.transform.Rotate(0,0, 500f * Time.deltaTime);
            transform.position += transform.up * (_surikenWeapon.Speed * Time.deltaTime);
        }

        [Inject] private void Construct(SurikenWeapon weapon) => _surikenWeapon = weapon;
    }
}