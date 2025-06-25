using MessagePipe;
using UnityEngine;
using static Messages.Messages;
using Zenject;
using R3;
using DG.Tweening;

namespace CameraHandler
{
    public class CameraShaker : MonoBehaviour
    {
        [Inject] private readonly ISubscriber<PlayerDamage> _playerDamage;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _shakeDuration = 1f; 
        [SerializeField] private float _shakeStrength = 2f;
        [SerializeField] private int _vibrato = 10; 
        [SerializeField] private float _randomness = 90f;

        private void Start()
        {
            _playerDamage.Subscribe(_ => ShakeCamera()).AddTo(this);
        }

        private void ShakeCamera()
        {
            _mainCamera.transform.DOKill();

            
            _mainCamera.transform.DOShakePosition(
                _shakeDuration,
                _shakeStrength,
                _vibrato,
                _randomness,
                false,
                true  
            ).OnComplete(() =>
            {
                _mainCamera.transform.localPosition = Vector3.zero;
            });
        }

    }
}
