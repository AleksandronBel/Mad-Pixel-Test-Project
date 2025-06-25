using Extentions;
using Infrastructure.Services;
using MessagePipe;
using Player;
using Projectile;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileItemView _projectilePrefab;
        private MessagePipeOptions _messagePipeOptions;

        public override void InstallBindings()
        {
            RegisterMessagePipe();
            BindServices();
            BindProjectilePool();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<InputService>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<PlayerStats>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindProjectilePool()
        {
            Container.BindMemoryPool<ProjectileItemView, ProjectilePool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransformGroup("Projectiles");
        }

        void RegisterMessagePipe()
        {
            _messagePipeOptions = Container.BindMessagePipe();
            GlobalMessagePipe.SetProvider(Container.AsServiceProvider());
            GetType().Assembly.GetTypes().ForEach(x =>
            {
                if (!x.IsAbstract || !x.IsSealed) return;

                System.Reflection.MethodInfo methodInfo = x.GetMethod("Install");

                if (methodInfo is null)
                    return;

                System.Reflection.ParameterInfo[] parameterInfos = methodInfo.GetParameters();

                if (parameterInfos.Length != 2)
                    return;

                if (parameterInfos[0].ParameterType != typeof(DiContainer) || parameterInfos[1].ParameterType != typeof(MessagePipeOptions))
                    return;

                methodInfo.Invoke(null, new object[] { Container, _messagePipeOptions });
            });
        }

    }
}