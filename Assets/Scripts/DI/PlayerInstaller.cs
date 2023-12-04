using GameCore;
using Player;
using Player.Weapons.Bow;
using Player.Weapons.FrozenBolt;
using Player.Weapons.Suriken;
using Player.Weapons.Trap;
using UnityEngine;
using Zenject;

namespace DI
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private PlayerHealth _playerHealth;
		[SerializeField] private SurikenWeapon _surikenWeapon;
		[SerializeField] private FrozenBoltWeapon _frozenBoltWeapon;
		[SerializeField] private TrapWeapon _trapWeapon;
		[SerializeField] private BowWeapon _bowWeapon;
		
		public override void InstallBindings()
		{
			Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
			Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
			Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
			Container.Bind<FrozenBoltWeapon>().FromInstance(_frozenBoltWeapon).AsSingle().NonLazy();
			Container.Bind<TrapWeapon>().FromInstance(_trapWeapon).AsSingle().NonLazy();
			Container.Bind<BowWeapon>().FromInstance(_bowWeapon).AsSingle().NonLazy();
		}
	}
}