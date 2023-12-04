using GameCore;
using UI.Fx;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private RandomSpawnPoint _randomSpawnPoint;
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        public override void InstallBindings()
        {
            Container.Bind<RandomSpawnPoint>().FromInstance(_randomSpawnPoint).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
        }
    }
}