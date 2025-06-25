using MessagePipe;
using UnityEngine;
using Zenject;

namespace Messages
{
    public static class Messages
    {
        public struct PlayerWin { }
        public struct ReloadStats { }
        public struct PlayerDead { }
        public struct PlayerShoot { }

        public struct PlayerDamage
        {
            public float DamageCount { get; }

            public PlayerDamage(float damageCount)
            {
                DamageCount = damageCount;
            }
        }

        public struct HitTarget
        {
            public float ScoreCount { get; }

            public HitTarget(float scoreCount)
            {
                ScoreCount = scoreCount;
            }
        }

        public struct ShootRequest
        {
            public Vector3 Position { get; }

            public ShootRequest(Vector3 position)
            {
                Position = position;
            }
        }

        public static void Install(DiContainer container, MessagePipeOptions options)
        {
            container.BindMessageBroker<PlayerWin>(options);
            container.BindMessageBroker<ReloadStats>(options);
            container.BindMessageBroker<PlayerDead>(options);
            container.BindMessageBroker<PlayerShoot>(options);
            container.BindMessageBroker<PlayerDamage>(options);
            container.BindMessageBroker<HitTarget>(options);
            container.BindMessageBroker<ShootRequest>(options);
        }
    }
}
