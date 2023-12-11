using GameCore.Pool;
using UnityEngine;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;

        public void Spawn(Vector3 position)
        {
            GameObject newExperience = _objectPool.GetFromPool();
            newExperience.transform.SetParent(transform);
            newExperience.transform.position = position;
        }
    }
}