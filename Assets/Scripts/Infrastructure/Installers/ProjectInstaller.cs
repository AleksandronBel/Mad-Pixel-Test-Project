using Extentions;
using Factories;
using Infrastructure.Services;
using MessagePipe;
using Player;
using Systems;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        MessagePipeOptions messagePipeOptions;

        public override void InstallBindings()
        {
            RegisterMessagePipe();
            BindServices();
            BindGameFactory();
        }

        void BindServices()
        {
            Container.BindInterfacesAndSelfTo<InputService>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<PlayerService>()
                     .AsSingle()
                     .NonLazy();
        }

        void BindGameFactory()
        {
            Container.Bind<GameFactory>()
                     .AsSingle()
                     .NonLazy();
        }

        void RegisterMessagePipe()
        {
            messagePipeOptions = Container.BindMessagePipe();
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

                methodInfo.Invoke(null, new object[] { Container, messagePipeOptions });
            });
        }

    }
}