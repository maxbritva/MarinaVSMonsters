using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private float _damage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                if(playerHealth == null) return;
                playerHealth.TakeDamage(_damage);
                playerHealth.OnHealthChanged?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}