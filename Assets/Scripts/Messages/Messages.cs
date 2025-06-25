using MessagePipe;
using UnityEngine;
using Zenject;

namespace Messages
{
    public static class Messages
    {
        public struct PlayerShoot { }

        public struct PlayerDamage
        {
            public float DamageCount { get; }

            public PlayerDamage(float damageCount)
            {
                DamageCount = damageCount;
            }
        }

        public struct AddScore
        {
            public float ScoreCount { get; }

            public AddScore(float scoreCount)
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
            container.BindMessageBroker<PlayerShoot>(options);
            container.BindMessageBroker<PlayerDamage>(options);
            container.BindMessageBroker<AddScore>(options);
            container.BindMessageBroker<ShootRequest>(options);
        }
    }
}
