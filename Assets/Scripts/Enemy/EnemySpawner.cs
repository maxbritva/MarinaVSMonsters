using System;
using System.Collections;
using GameCore;
using GameCore.Pool;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private Transform _minPoint, _maxPoint;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private ObjectPool _enemyPool;
        private PlayerController _playerController;
        private RandomSpawnPoint _randomSpawnPoint;
        private WaitForSeconds _interval;
        private Coroutine _spawnCoroutine;

        private void Start() => _interval = new WaitForSeconds(_timeToSpawn);

        public void Activate() => _spawnCoroutine = StartCoroutine(Spawn());

        public void Deactivate()
        {
            if(_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        [Inject] private void Construct(PlayerController controller, RandomSpawnPoint point)
        {
            _playerController = controller;
            _randomSpawnPoint = point;
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                transform.position = _playerController.transform.position;
                GameObject newEnemy = _enemyPool.GetFromPool();
                newEnemy.transform.SetParent(_enemyContainer);
                newEnemy.transform.position = _randomSpawnPoint.GetRandomSpawnPoint(_minPoint, _maxPoint);
                yield return _interval;
            }
        }
    }
}