﻿using System.Collections;
using Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Weapons
{
    public class Throw : MonoBehaviour
    {
        protected WaitForSeconds Timer;
        protected float Damage;

        protected virtual void OnEnable() => StartCoroutine(TimerToHide());

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if(enemy == null) return;
                float damage = Random.Range(Damage / 1.5f, Damage * 1.8f);
                enemy.TakeDamage(damage);
            }
        }

        private IEnumerator TimerToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }
    }
}