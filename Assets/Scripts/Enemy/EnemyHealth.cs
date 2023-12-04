using System.Collections;
using GameCore.Health;
using UI.Fx;
using UnityEngine;
using Zenject;


namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        private WaitForSeconds _tick = new WaitForSeconds(0.5f);
        private DamageTextSpawner _damageTextSpawner;
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            _damageTextSpawner.Activate(transform, (int)damage);
            if(_currentHealth <= 0 == false) return;
            gameObject.SetActive(false);
        }

        public void Burn(float damage) => StartCoroutine(GetBurn(damage));

        private IEnumerator GetBurn(float damage)
        {
            if(gameObject.activeSelf == false)
                yield break;
            float tickDamage = damage / 3f;
            if (tickDamage < 1)
                tickDamage = 1;
            for (int i = 0; i < 5; i++)
            {
                TakeDamage(tickDamage);
                _damageTextSpawner.Activate(transform, (int)damage);
                yield return _tick;
            }
        }

        [Inject] private void Construct(DamageTextSpawner textSpawner) => _damageTextSpawner = textSpawner;
    }
}