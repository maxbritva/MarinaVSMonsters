using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    [Serializable]
    public class Level
    {
        [SerializeField] private List<EnemySpawner> _spawners = new List<EnemySpawner>();

        public void Activate()
        {
            for (int i = 0; i < _spawners.Count; i++) 
                _spawners[i].Activate();
        }

        public void Deactivate()
        {
            for (int i = 0; i < _spawners.Count; i++) 
                _spawners[i].Deactivate();
        }
    }
    
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels = new List<Level>();
        public Action OnLevelChanged;
        private GameTimer _gameTimer;
        [Inject] private DiContainer _container;

        private void Awake()
        {
            for (int i = 0; i < _levels.Count; i++) 
                _container.Inject(_levels[i]);
        }

        private void Start() => Activate();

        private void OnEnable() => OnLevelChanged += LevelUp;

        private void OnDisable() => OnLevelChanged -= LevelUp;

        public void Activate() => _levels[_gameTimer.Minutes].Activate();
        
        public void Deactivate() => _levels[_gameTimer.Minutes].Deactivate();

        private void LevelUp()
        {
            _levels[_gameTimer.Minutes-1].Deactivate();
            _levels[_gameTimer.Minutes].Activate();
        }

        [Inject] private void Construct(GameTimer timer) => _gameTimer = timer;
    }
}