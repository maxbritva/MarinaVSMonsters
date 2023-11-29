using UnityEngine;
using Zenject;

namespace Player.Weapons.Bow
{
    public class Arrow : Throw
    {
        private BowWeapon _bowWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_bowWeapon.Duration);
         Damage = _bowWeapon.Damage;
        }
        
        private void Update() => transform.position += transform.up * (-1 * (_bowWeapon.Speed * Time.deltaTime));

        [Inject] private void Construct(BowWeapon bowWeapon) => _bowWeapon = bowWeapon;
    }
}