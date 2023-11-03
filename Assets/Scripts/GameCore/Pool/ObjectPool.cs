using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCore.Pool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] private GameObject _prefab;
        private List<GameObject> _objectPool = new List<GameObject>();
        [Inject] private DiContainer _container;
        
        public GameObject GetFromPool()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if(_objectPool[i].activeInHierarchy) continue;
                SetActiveObject(_objectPool[i], true);
                return _objectPool[i];
            }
            GameObject newObject = Create();
            SetActiveObject(newObject, true);
            return newObject;
        }
        public GameObject Create()
        {
            GameObject newObject = _container.InstantiatePrefab(_prefab);
            SetActiveObject(newObject, false);
            _objectPool.Add(newObject);
            return newObject;
        }

        private void SetActiveObject(GameObject objectToRelease, bool value) => objectToRelease.gameObject.SetActive(value);
    }
}