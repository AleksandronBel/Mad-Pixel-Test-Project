using MessagePipe;
using UnityEngine;
using Zenject;

namespace Messages
{
    public static class Messages
    {
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
            container.BindMessageBroker<ShootRequest>(options);
        }
    }
}
