using Factories;
using MessagePipe;
using Player;
using Projectile;
using R3;
using UnityEngine;
using Zenject;
using static Messages.Messages;

namespace Systems
{
    public class ShootingSystem : MonoBehaviour
    {
        [Inject] private ISubscriber<ShootRequest> _shootSubscriber;

        [SerializeField] private ProjectileItemView _projectilePrefab;
        [SerializeField] private Camera _camera;

        private PlayerService _playerState;
        private GameFactory _gameFactory;

        [Inject]
        private void Construct(PlayerService state, GameFactory gameFactory)
        {
            _playerState = state;
            _gameFactory = gameFactory;

            _shootSubscriber.Subscribe(message => OnShootRequested(message.Position)).AddTo(this);
        }

        private void OnShootRequested(Vector3 position)
        {
            var ray = _camera.ScreenPointToRay(position);

            if (_playerState.Ammo.Value <= 0) return;

            _playerState.Ammo.Value--;

            var projectile = _gameFactory.Instantiate(_projectilePrefab, null);
            projectile.Launch(_camera.transform.position, ray.direction);
        }
    }
}
