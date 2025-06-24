using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using Zenject;
using static Messages.Messages;

namespace Infrastructure.Services
{
    public class InputService : ITickable
    {
        [Inject] private readonly IPublisher<ShootRequest> _shootPublisher;
        private Camera _camera;

        [Inject]
        private void Construct()
        {
            _camera = Camera.main;
        }

        public void Tick()
        {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            Fire(Input.GetTouch(0).position);
#else
            if (Input.GetMouseButtonDown(0))
                Fire(Input.mousePosition);
#endif
        }

        private void Fire(Vector3 screenPos)
        {
            _shootPublisher.Publish(new(screenPos));
        }
    }
}