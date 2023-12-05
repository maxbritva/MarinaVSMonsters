﻿using GameCore;
using GameCore.LevelSystem;
using UI.Fx;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private RandomSpawnPoint _randomSpawnPoint;
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        public override void InstallBindings()
        {
            Container.Bind<RandomSpawnPoint>().FromInstance(_randomSpawnPoint).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy();
        }
    }
}