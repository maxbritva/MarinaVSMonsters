using System.Collections;
using GameCore.Health;
using UnityEngine;


namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        private WaitForSeconds _tick = new WaitForSeconds(0.5f);
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
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
                yield return _tick;
            }
        }
    }
}