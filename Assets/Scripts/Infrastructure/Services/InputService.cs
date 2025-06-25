using MessagePipe;
using static Messages.Messages;
using Zenject;
using UnityEngine;

namespace Infrastructure.Services
{
    public class InputService : ITickable
    {
        [Inject] private readonly IPublisher<ShootRequest> _shootPublisher;

        private System.Action _handleInput;

        [Inject]
        private void Construct()
        {
            InitializeInputHandler();
        }

        private void InitializeInputHandler()
        {
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.WebGLPlayer) 
            {
                _handleInput = HandleTouchInput;
            }
            else
            {
                _handleInput = HandleMouseInput;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Fire(Input.GetTouch(0).position);
            }
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire(Input.mousePosition);
            }
        }

        public void Tick()
        {
            _handleInput?.Invoke();
        }

        private void Fire(Vector3 screenPos)
        {
            _shootPublisher.Publish(new ShootRequest(screenPos));
        }
    }
}
