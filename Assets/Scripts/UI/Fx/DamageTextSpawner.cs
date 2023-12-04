using System.Collections;
using GameCore.Pool;
using TMPro;
using UnityEngine;

namespace UI.Fx
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _damageTextPool;
        private readonly WaitForSeconds _wait = new WaitForSeconds(0.05f);

        public void Activate(Transform target, int damage)
        {
            GameObject newDamageText = _damageTextPool.GetFromPool();
            newDamageText.transform.SetParent(transform);
            newDamageText.transform.position = target.position + NewRandomPosition();
            if (newDamageText.TryGetComponent(out TextMeshProUGUI damageText))
            {
                damageText.text = damage.ToString();
                float damageSize = damage / 15f;
                damageText.fontSize = Mathf.Clamp(damageSize, 1f, 5f);
                newDamageText.SetActive(true);
                StartCoroutine(DamageTexSetup(damageText, newDamageText));
            }
        }
        private Vector3 NewRandomPosition() => new Vector3(Random.Range(-1.5f,1.5f),Random.Range(-1.5f,1.5f));

        private IEnumerator DamageTexSetup(TextMeshProUGUI text, GameObject targetEffect)
        {
            Color color = text.color;
            color.a = 1f;
            for (float f = 1f; f >= - 0.05f; f-=0.05f)
            {
                text.color = color;
                color.a = f;
                yield return _wait;
            }
            targetEffect.SetActive(false);
        }
    }
}