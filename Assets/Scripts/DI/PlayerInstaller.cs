using Player;
using UnityEngine;
using Zenject;

namespace DI
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private PlayerHealth _playerHealth;
		public override void InstallBindings()
		{
			Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
			Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
		}
	}
}