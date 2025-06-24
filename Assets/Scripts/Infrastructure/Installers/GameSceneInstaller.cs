using Systems;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private ShootingSystem _shootingSystem;

        public override void InstallBindings()
        {
            Container.Bind<ShootingSystem>()
                     .FromInstance(_shootingSystem)
                     .AsSingle()
                     .NonLazy();
        }
    }
}
