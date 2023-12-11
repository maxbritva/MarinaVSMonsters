using GameCore;
using GameCore.ExperienceSystem;
using GameCore.LevelSystem;
using UI.Fx;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceSystem _experienceSystem;
        [SerializeField] private RandomSpawnPoint _randomSpawnPoint;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        public override void InstallBindings()
        {
            Container.Bind<RandomSpawnPoint>().FromInstance(_randomSpawnPoint).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<ExperienceSpawner>().FromInstance(_experienceSpawner).AsSingle().NonLazy();
            Container.Bind<ExperienceSystem>().FromInstance(_experienceSystem).AsSingle().NonLazy();
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy();
        }
    }
}