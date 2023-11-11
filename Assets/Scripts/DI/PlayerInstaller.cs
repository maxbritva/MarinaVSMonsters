﻿using GameCore;
using Player;
using Player.Weapons.Suriken;
using UnityEngine;
using Zenject;

namespace DI
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private PlayerHealth _playerHealth;
		[SerializeField] private SurikenWeapon _surikenWeapon;
		[SerializeField] private RandomSpawnPoint _randomSpawnPoint;
		public override void InstallBindings()
		{
			Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
			Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
			Container.Bind<RandomSpawnPoint>().FromInstance(_randomSpawnPoint).AsSingle().NonLazy();
			Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
		}
	}
}